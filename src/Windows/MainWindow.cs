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

            if (!string.IsNullOrWhiteSpace(SelectedGame.LaunchURL))
                if (ImGui.Button("Play!"))
                    URL.Open(SelectedGame.LaunchURL);

            if (!string.IsNullOrWhiteSpace(SelectedGame.LaunchPath))
            {
                if (ImGui.Button("Play Locally!"))
                {
                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = SelectedGame.LaunchPath,
                        Arguments = SelectedGame.LaunchArgs,
                    };
                    process.Start();
                }
            }
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
