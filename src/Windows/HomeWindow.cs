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

        foreach (var game in games)
        {
            var icon = game.CoverArt != null ? game.CoverArt.Value : MissingCoverArt;

            ImGui.TableNextColumn();

            if (rlImGui.ImageButton(game.Name, icon))
                SelectedGame = game;
        }

        ImGui.EndTable();
    }
}
