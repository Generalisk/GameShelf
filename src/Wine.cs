// The Wine script is to help the GameCollector package run on Linux,
// so it isn't nessecary to build the script for other platforms
#if PLATFORM_LINUX
using GameCollector.Wine;
using NexusMods.Paths;

namespace GameShelf;

internal static class Wine
{
    public static AWinePrefix GetPrefix()
    {
        AWinePrefix? prefix = null;

        var prefixManager = new DefaultWinePrefixManager(FileSystem.Shared);
        var prefixes = prefixManager.FindPrefixes();

        foreach (var result in prefixes)
        {
            result.Switch(_prefix =>
            {
                prefix = _prefix;
            }, error =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            });
        }

        if (prefix != null)
            return prefix;

        // Oh no, no prefix could be found, guess i'll die then ¯\_(ツ)_/¯
        throw new NullReferenceException();
    }
}
#endif
