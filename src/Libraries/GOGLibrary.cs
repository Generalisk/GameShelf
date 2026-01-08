using GameCollector.StoreHandlers.GOG;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class GOGLibrary : StoreLibrary<GOGGame, GOGHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.GOG;

    protected override GOGHandler GetHandler()
    {
        Console.WriteLine("Initializing GOG Handler...");

#if PLATFORM_WINDOWS
        var fileSystem = FileSystem.Shared;
        var registry = WindowsRegistry.Shared;
#else
        var prefix = Wine.GetPrefix();

        var fileSystem = prefix.CreateOverlayFileSystem(FileSystem.Shared);
        var registry = prefix.CreateRegistry(FileSystem.Shared);
#endif

        var handler = new GOGHandler(registry, fileSystem);

        Console.WriteLine("GOG Handler Initialized!");

        return handler;
    }

    public override GOGGame? Get(object id)
    {
        var gameid = GOGGameId.From(Convert.ToInt64(id));
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override GOGGame[] GetAll()
    {
        var games = new List<GOGGame>();

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
            if (!game.IsInstalled)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} is not installed, skipping...", game.GameName);
                Console.ResetColor();
                continue;
            }

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
