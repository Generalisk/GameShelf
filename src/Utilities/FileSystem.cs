using System.Diagnostics;

namespace GameShelf.Utilities;

internal static class FileSystem
{
    public static void OpenFile(string path)
    {
        Process.Start(new ProcessStartInfo()
        {
#if PLATFORM_WINDOWS
            FileName = "explorer.exe",
            Arguments = "\"" + path + "\"",
#elif PLATFORM_LINUX
            FileName = "/bin/bash",
            Arguments = "xdg-open \"" + path + "\"",
#endif
        });
    }
}
