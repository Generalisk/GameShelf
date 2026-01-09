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

        var previousName = "";
        int count = 0;

        foreach (var game in Games)
        {
            var icon = game.Icon != null ? game.Icon.Value : MissingIcon;

            var posX = ImGui.GetCursorPosX();

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
                count = 0;
            }

            if (ImGui.Button(new string(' ', 3) + name))
                SelectedGame = game;

            ImGui.SameLine();
            ImGui.SetCursorPosX(posX);

            rlImGui.Image(icon);
        }
    }
}
