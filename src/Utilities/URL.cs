using System.Diagnostics;

namespace GameShelf.Utilities;

internal static class URL
{
    public static void Open(string url) =>
        Process.Start(new ProcessStartInfo()
        {
#if PLATFORM_WINDOWS
            FileName = "cmd.exe",
            Arguments = "/C start " + url,
#elif PLATFORM_LINUX
            FileName = "/bin/bash",
            Arguments = "xdg-open " + url,
#endif
        });
}
