using GameShelf.Utilities;
using ImGuiNET;

namespace GameShelf;

internal static class Menu
{
    public static void Draw()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("GameShelf"))
            {
                if (ImGui.MenuItem("Refresh"))
                    RefreshAll();

                ImGui.Separator();

                if (ImGui.MenuItem("Quit"))
                    Close = true;

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Help"))
            {
                if (ImGui.MenuItem("Report Issue"))
                    URL.Open("https://github.com/Generalisk/GameShelf/issues/new");

                ImGui.Separator();

                if (ImGui.MenuItem("Source Code"))
                    URL.Open("https://github.com/Generalisk/GameShelf");

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }
}
