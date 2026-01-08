// It's Xbox, what did you expect? -Generalisk, 02/01/26
#if PLATFORM_WINDOWS
using GameCollector.StoreHandlers.Xbox;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class XboxLibrary : StoreLibrary<XboxGame, XboxHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.Xbox;

    protected override XboxHandler GetHandler()
    {
        Console.WriteLine("Initializing Xbox Handler...");

        var handler = new XboxHandler(FileSystem.Shared);

        Console.WriteLine("Xbox Handler Initialized!");

        return handler;
    }

    public override XboxGame? Get(object id)
    {
        var gameid = XboxGameId.From(id.ToString());
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override XboxGame[] GetAll()
    {
        var games = new List<XboxGame>();

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
#endif
