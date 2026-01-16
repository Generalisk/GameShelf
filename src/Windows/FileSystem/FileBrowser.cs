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

    protected override void Init()
    {
        files = Directory.GetFiles(directory);
        dirs = Directory.GetDirectories(directory);
    }

    public override void Draw()
    {
        ImGui.PushItemWidth(-1);
        if (ImGui.InputText("  Path", ref directory, uint.MaxValue, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            if (!Directory.Exists(directory)) return;

            files = Directory.GetFiles(directory);
            dirs = Directory.GetDirectories(directory);
        }
        ImGui.PopItemWidth();

        ImGui.BeginChild("File List", ImGui.GetWindowSize() - new Vector2(8, 51));
        foreach (var dir in dirs)
        {
            if (ImGui.Button(new DirectoryInfo(dir).Name))
            {
                directory = dir;

                files = Directory.GetFiles(directory);
                dirs = Directory.GetDirectories(directory);
            }
        }

        foreach (var file in files)
        {
            if (ImGui.Button(new FileInfo(file).Name))
                Utilities.FileSystem.OpenFile(file);
        }
        ImGui.EndChild();
    }
}
