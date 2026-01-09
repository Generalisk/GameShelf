using GameShelf.Utilities;
using ImGuiNET;
using System.Diagnostics;

namespace GameShelf.Windows;

internal class GameWindow : StaticWindow
{
    public override string Title { get; } = "Game Window";

    public override void Draw()
    {
        ImGui.Text(SelectedGame.Name);

        bool showPlayButton = !string.IsNullOrWhiteSpace(SelectedGame.LaunchURL);
        bool showPlayButtonLocal = !string.IsNullOrWhiteSpace(SelectedGame.LaunchPath);

        if (showPlayButton)
            if (ImGui.Button("Play!"))
                URL.Open(SelectedGame.LaunchURL);

        if (showPlayButton && showPlayButtonLocal)
            ImGui.SameLine();

        if (showPlayButtonLocal)
            if (ImGui.Button("Play Locally!"))
                Process.Start(new ProcessStartInfo()
                {
                    FileName = SelectedGame.LaunchPath,
                    Arguments = SelectedGame.LaunchArgs,
                });
    }
}
