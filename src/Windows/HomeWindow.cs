using ImGuiNET;
using rlImGui_cs;

namespace GameShelf.Windows;

internal class HomeWindow : StaticWindow
{
    public override string Title { get; } = "Main Window";

    internal int width = 0;

    public override void Draw()
    {
        int columns = width / 200;
        ImGui.BeginTable("Table", columns);

        var previousName = "";
        int count = 1;

        foreach (var game in games)
        {
            var icon = game.CoverArt != null ? game.CoverArt.Value : MissingCoverArt;

            ImGui.TableNextColumn();

            // This to prevent issues with ImGui involving duplicate names
            string name = game.Name;

            if (name == previousName)
            {
                name += string.Format(" ({0})", count + 1);
                count++;
            }
            else
            {
                previousName = game.Name;
                count = 1;
            }

            if (rlImGui.ImageButton(name, icon))
                SelectedGame = game;
        }

        ImGui.EndTable();
    }
}
