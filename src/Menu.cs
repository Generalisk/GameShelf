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

            ImGui.EndMainMenuBar();
        }
    }
}
