using GameShelf;
using GameShelf.Libraries;
using GameShelf.Windows;
using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using System.Numerics;

// Load Fallback Graphics
ReloadMissingIcon();
ReloadMissingCoverArt();

// Retrieve Games
RefreshAll();

// Init Window
Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);

Raylib.InitWindow(1280, 720, "GameShelf");

Raylib.MaximizeWindow();

rlImGui.Setup();

Shared.HomeWindow = new HomeWindow();
Shared.GameWindow = new GameWindow();
Shared.GameInfoWindow = new GameInfoWindow();
Shared.GameListWindow = new GameListWindow();

// Main Loop
while (!Raylib.WindowShouldClose() && !Close)
{
    // Update
    if (Raylib.IsWindowResized())
    {
        ReloadMissingCoverArt();

        foreach (var game in Games)
            game.LoadCoverArt();
    }

    LoadQueue.Next();

    // Draw
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.Black);

    rlImGui.Begin();

    Menu.Draw();

    int x = 0;
    int y = 18;
    int width = Raylib.GetRenderWidth();
    int height = Raylib.GetRenderHeight() - y;

    int windowWidth = Convert.ToInt32(width / 4.5f);
    if (ImGui.Begin(Shared.GameListWindow.Title, Shared.GameListWindow.Flags))
    {
        ImGui.SetWindowPos(new Vector2(x, y));
        ImGui.SetWindowSize(new Vector2(windowWidth, height));

        Shared.GameListWindow.Draw();
    }
    ImGui.End();

    x += windowWidth;
    width -= x;

    if (SelectedGame == null)
    {
        if (ImGui.Begin(Shared.HomeWindow.Title, Shared.HomeWindow.Flags))
        {
            ImGui.SetWindowPos(new Vector2(x, y));
            ImGui.SetWindowSize(new Vector2(width, height));

            Shared.HomeWindow.width = width;
            Shared.HomeWindow.Draw();
        }
        ImGui.End();
    }
    else
    {
        var coverArt = SelectedGame.CoverArt != null ? SelectedGame.CoverArt.Value : MissingCoverArt;

        var infoWidth = coverArt.Width + 18;

        if (ImGui.Begin(Shared.GameInfoWindow.Title, Shared.GameInfoWindow.Flags))
        {
            ImGui.SetWindowPos(new Vector2((x + width) - infoWidth, y));
            ImGui.SetWindowSize(new Vector2(infoWidth, height));

            Shared.GameInfoWindow.Draw();
        }
        ImGui.End();

        width -= infoWidth;

        if (ImGui.Begin(Shared.GameWindow.Title, Shared.GameWindow.Flags))
        {
            ImGui.SetWindowPos(new Vector2(x, y));
            ImGui.SetWindowSize(new Vector2(width, height));

            Shared.GameWindow.Draw();
        }
        ImGui.End();
    }

    rlImGui.End();

#if DEBUG
    Raylib.DrawFPS(10, 10);
#endif

    Raylib.EndDrawing();
}

// De-initializion: unload everything!
Shared.HomeWindow.Dispose();
Shared.GameWindow.Dispose();
Shared.GameInfoWindow.Dispose();
Shared.GameListWindow.Dispose();

rlImGui.Shutdown();
Raylib.CloseWindow();

new LocalLibrary().Save();

while (Games.Length > 0)
    Games[0].Dispose();

Raylib.UnloadTexture(MissingIcon);
Raylib.UnloadTexture(MissingCoverArt);
