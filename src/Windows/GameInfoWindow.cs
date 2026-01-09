using ImGuiNET;
using rlImGui_cs;

namespace GameShelf.Windows;

internal class GameInfoWindow : StaticWindow
{
    public override string Title { get; } = "Game Info Window";

    public override void Draw()
    {
        var coverArt = SelectedGame.CoverArt != null ? SelectedGame.CoverArt.Value : MissingCoverArt;

        rlImGui.Image(coverArt);

        ImGui.Text(string.Format("Source: {0}", Enum.GetName(SelectedGame.Source)));
    }
}
