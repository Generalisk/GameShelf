using GameShelf.Utilities;
using ImGuiNET;
using rlImGui_cs;
using System.Diagnostics;

namespace GameShelf.Windows;

internal class MainWindow : StaticWindow
{
    public override string Title { get; } = "Main Window";

    internal int width = 0;

    public override void Draw()
    {
        if (SelectedGame != null)
        {
            var coverArt = SelectedGame.CoverArt != null ? SelectedGame.CoverArt.Value : MissingCoverArt;

            rlImGui.Image(coverArt);

            ImGui.Text(SelectedGame.Name);

            ImGui.Text(string.Format("Source: {0}", Enum.GetName(SelectedGame.Source)));

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
        else
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
}
