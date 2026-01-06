using GameCollector.StoreHandlers.RobotCache;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class RobotCacheLibrary : Library<RobotCacheGame, RobotCacheHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.RobotCache;

    protected override RobotCacheHandler GetHandler()
    {
        Console.WriteLine("Initializing Robot Cache Handler...");

#if PLATFORM_WINDOWS
        var fileSystem = FileSystem.Shared;
#else
        var prefix = Wine.GetPrefix();

        var fileSystem = prefix.CreateOverlayFileSystem(FileSystem.Shared);
#endif

        var handler = new RobotCacheHandler(fileSystem);

        Console.WriteLine("Robot Cache Handler Initialized!");

        return handler;
    }

    public override RobotCacheGame? Get(object id)
    {
        var gameid = RobotCacheGameId.From(Convert.ToInt32(id));
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override RobotCacheGame[] GetAll()
    {
        var games = new List<RobotCacheGame>();

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
                LaunchPath = launchPath,
                LaunchArgs = game.LaunchArgs,
            };
        }
    }
}
