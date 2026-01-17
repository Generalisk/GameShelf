using ImGuiNET;
using System.Numerics;

namespace GameShelf.Windows.FileSystem;

internal class FileBrowser : Window
{
    public override string Title { get; } = "File Browser";

    public override ImGuiWindowFlags Flags { get; } = ImGuiWindowFlags.NoScrollbar;

    public override Vector2 MinSize { get; } = new Vector2(720, 480);

    private string directory = Directory.GetCurrentDirectory();

    private string? selectedFile = null;

    private string[] files = { };
    private string[] dirs = { };

    // System Folders
    private string[] SystemFolders { get; } =
    {
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads",
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
        Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
        Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
#if PLATFORM_WINDOWS
        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
#endif
    };

    private string[] SystemFolderNames { get; } =
    {
        "Home",
        "Desktop",
        "Downloads",
        "Documents",
        "Pictures",
        "Music",
        "Videos",
#if PLATFORM_WINDOWS
        "Program Files",
#endif
    };

    protected override void Init() => SetDirectory(directory);

    public override void Draw()
    {
        if (ImGui.Button("<"))
            SetDirectory(new DirectoryInfo(directory).Parent.FullName);

        ImGui.SameLine();

        ImGui.PushItemWidth(-1);
        if (ImGui.InputText("  Path", ref directory, uint.MaxValue, ImGuiInputTextFlags.EnterReturnsTrue))
            SetDirectory(directory);
        ImGui.PopItemWidth();

        var contentsY = ImGui.GetCursorPosY();
        var contentsHeight = ImGui.GetWindowHeight() - 128;

        // Draw directory shortcuts
        var dirShortcutsWidth = 128;
        ImGui.BeginChild("Directory Shortcuts", new Vector2(dirShortcutsWidth - 12, contentsHeight));

        // System folders
        if (ImGui.CollapsingHeader("System", ImGuiTreeNodeFlags.DefaultOpen))
            for (int i = 0; i < SystemFolders.Length; i++)
                if (ImGui.Button(SystemFolderNames[i]))
                    SetDirectory(SystemFolders[i]);

        // Drives
        if (ImGui.CollapsingHeader("Drives", ImGuiTreeNodeFlags.DefaultOpen))
        {
            var drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
                if (ImGui.Button(drive.RootDirectory.FullName))
                    SetDirectory(drive.RootDirectory.FullName);
        }

        ImGui.EndChild();

        // Draw file list
        ImGui.SetCursorPos(new Vector2(dirShortcutsWidth, contentsY));
        ImGui.BeginChild("File List", new Vector2(ImGui.GetWindowWidth() - (dirShortcutsWidth + 2), contentsHeight));

        foreach (var dir in dirs)
            if (ImGui.Button(new DirectoryInfo(dir).Name))
                if (selectedFile == dir)
                    SetDirectory(dir);
                else
                    selectedFile = dir;

        foreach (var file in files)
            if (ImGui.Button(new FileInfo(file).Name))
                if (selectedFile == file)
                    Utilities.FileSystem.OpenFile(file);
                else
                    selectedFile = file;

        ImGui.EndChild();

        ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 8);

        // Draw selected file info
        if (selectedFile == null)
        {
            ImGui.Text(" ");
            ImGui.Text(" ");

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 8);

            ImGui.BeginDisabled();
            ImGui.Button("Select!");
            ImGui.EndDisabled();
        }
        else
        {
            ImGui.Text(new FileInfo(selectedFile).Name);
            ImGui.Text(selectedFile);

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 8);

            if (ImGui.Button("Select!"))
                if (Directory.Exists(selectedFile))
                    SetDirectory(selectedFile);
                else
                    Utilities.FileSystem.OpenFile(selectedFile);
        }

        ImGui.SameLine();

        if (ImGui.Button("Cancel!"))
            Dispose();
    }

    public void SetDirectory(string directory)
    {
        if (!Directory.Exists(directory)) return;

        this.directory = directory;

        files = Directory.GetFiles(directory);
        dirs = Directory.GetDirectories(directory);
    }
}
