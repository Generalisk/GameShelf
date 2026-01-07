using ImGuiNET;
using System.Diagnostics;

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
                {
                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
#if PLATFORM_WINDOWS
                        FileName = "cmd.exe",
                        Arguments = "/C start https://github.com/Generalisk/GameShelf/issues/new",
#elif PLATFORM_LINUX
                        FileName = "/bin/bash",
                        Arguments = "xdg-open https://github.com/Generalisk/GameShelf/issues/new",
#endif
                    };
                    process.Start();
                }

                ImGui.Separator();

                if (ImGui.MenuItem("Source Code"))
                {
                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo()
                    {
#if PLATFORM_WINDOWS
                        FileName = "cmd.exe",
                        Arguments = "/C start https://github.com/Generalisk/GameShelf",
#elif PLATFORM_LINUX
                        FileName = "/bin/bash",
                        Arguments = "xdg-open https://github.com/Generalisk/GameShelf",
#endif
                    };
                    process.Start();
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }
}
