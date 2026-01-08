using GameCollector.StoreHandlers.BigFish;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class BigFishLibrary : StoreLibrary<BigFishGame, BigFishHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.BigFish;

    protected override BigFishHandler GetHandler()
    {
        Console.WriteLine("Initializing Big Fish Handler...");

#if PLATFORM_WINDOWS
        var fileSystem = FileSystem.Shared;
        var registry = WindowsRegistry.Shared;
#else
        var prefix = Wine.GetPrefix();

        var fileSystem = prefix.CreateOverlayFileSystem(FileSystem.Shared);
        var registry = prefix.CreateRegistry(FileSystem.Shared);
#endif

        var handler = new BigFishHandler(registry, fileSystem);

        Console.WriteLine("Big Fish Handler Initialized!");

        return handler;
    }

    public override BigFishGame? Get(object id)
    {
        var gameid = BigFishGameId.From(id.ToString());
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override BigFishGame[] GetAll()
    {
        var games = new List<BigFishGame>();

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
