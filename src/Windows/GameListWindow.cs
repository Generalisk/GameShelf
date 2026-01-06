using ImGuiNET;
using rlImGui_cs;

namespace GameShelf.Windows;

internal class GameListWindow : StaticWindow
{
    public override string Title { get; } = "Game List";

    public override void Draw()
    {
        if (ImGui.Button("Home"))
            SelectedGame = null;

        foreach (var game in Games)
        {
            var icon = game.Icon != null ? game.Icon.Value : MissingIcon;

            var posX = ImGui.GetCursorPosX();

            if (ImGui.Button(new string(' ', 3) + game.Name))
                SelectedGame = game;

            ImGui.SameLine();
            ImGui.SetCursorPosX(posX);

            rlImGui.Image(icon);
        }
    }
}
