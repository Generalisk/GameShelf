using GameShelf;
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

Shared.MainWindow = new MainWindow();
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

    if (ImGui.Begin(Shared.MainWindow.Title, Shared.MainWindow.Flags))
    {
        ImGui.SetWindowPos(new Vector2(x, y));
        ImGui.SetWindowSize(new Vector2(width, height));

        Shared.MainWindow.width = width;
        Shared.MainWindow.Draw();
    }
    ImGui.End();

    rlImGui.End();

#if DEBUG
    Raylib.DrawFPS(10, 10);
#endif

    Raylib.EndDrawing();
}

// De-initializion: unload everything!
Shared.MainWindow.Dispose();
Shared.GameListWindow.Dispose();

rlImGui.Shutdown();
Raylib.CloseWindow();

while (Games.Length > 0)
    Games[0].Dispose();

Raylib.UnloadTexture(MissingIcon);
Raylib.UnloadTexture(MissingCoverArt);
