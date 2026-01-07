using GameCollector.StoreHandlers.Legacy;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class LegacyLibrary : Library<LegacyGame, LegacyHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.Legacy;

    protected override LegacyHandler GetHandler()
    {
        Console.WriteLine("Initializing Legacy Handler...");

#if PLATFORM_WINDOWS
        var fileSystem = FileSystem.Shared;
        var registry = WindowsRegistry.Shared;
#else
        var prefix = Wine.GetPrefix();

        var fileSystem = prefix.CreateOverlayFileSystem(FileSystem.Shared);
        var registry = prefix.CreateRegistry(FileSystem.Shared);
#endif

        var handler = new LegacyHandler(registry, fileSystem);

        Console.WriteLine("Legacy Handler Initialized!");

        return handler;
    }

    public override LegacyGame? Get(object id)
    {
        var gameid = LegacyGameId.From(id.ToString());
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override LegacyGame[] GetAll()
    {
        var games = new List<LegacyGame>();

        var results = handler.FindAllGames();

        foreach (var result in results)
        {
            result.Switch(game =>
            {
                games.Add(game);

                Console.WriteLine(game.GameName);
            }, error =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
                Console.ResetColor();
            });
        }

        return games.ToArray();
    }

    public override void Refresh()
    {
        Clear();

        var games = GetAll();
        foreach (var game in games)
        {
            var icon = "";
            var launchPath = "";

            if (game.Icon != default)
                icon = game.Icon.GetFullPath();

            if (game.Launch != default)
                launchPath = game.Launch.GetFullPath();

            new GameInfo()
            {
                Name = game.GameName,
                Source = Source,
                IconPath = icon,
                LaunchURL = game.LaunchUrl,
                LaunchPath = launchPath,
                LaunchArgs = game.LaunchArgs,
            };
        }
    }
}
