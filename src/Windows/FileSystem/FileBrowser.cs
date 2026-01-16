using ImGuiNET;
using System.Numerics;

namespace GameShelf.Windows.FileSystem;

internal class FileBrowser : Window
{
    public override string Title { get; } = "File Browser";

    public override ImGuiWindowFlags Flags { get; } = ImGuiWindowFlags.NoScrollbar;

    public override Vector2 MinSize { get; } = new Vector2(720, 480);

    private string directory = Directory.GetCurrentDirectory();

    private string[] files = { };
    private string[] dirs = { };

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

        // Draw File List
        ImGui.BeginChild("File List", ImGui.GetWindowSize() - new Vector2(8, 51));
        foreach (var dir in dirs)
            if (ImGui.Button(new DirectoryInfo(dir).Name))
                SetDirectory(dir);

        foreach (var file in files)
            if (ImGui.Button(new FileInfo(file).Name))
                Utilities.FileSystem.OpenFile(file);

        ImGui.EndChild();
    }

    public void SetDirectory(string directory)
    {
        if (!Directory.Exists(directory)) return;

        this.directory = directory;

        files = Directory.GetFiles(directory);
        dirs = Directory.GetDirectories(directory);
    }
}
