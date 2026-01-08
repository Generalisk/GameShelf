using GameCollector.StoreHandlers.Amazon;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class AmazonLibrary : StoreLibrary<AmazonGame, AmazonHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.Amazon;

    protected override AmazonHandler GetHandler()
    {
        Console.WriteLine("Initializing Amazon Handler...");

#if PLATFORM_WINDOWS
        var fileSystem = FileSystem.Shared;
        var registry = WindowsRegistry.Shared;
#else
        var prefix = Wine.GetPrefix();

        var fileSystem = prefix.CreateOverlayFileSystem(FileSystem.Shared);
        var registry = prefix.CreateRegistry(FileSystem.Shared);
#endif

        var handler = new AmazonHandler(registry, fileSystem);

        Console.WriteLine("Amazon Handler Initialized!");

        return handler;
    }

    public override AmazonGame? Get(object id)
    {
        var gameid = AmazonGameId.From(id.ToString());
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override AmazonGame[] GetAll()
    {
        var games = new List<AmazonGame>();

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
