// The GameCollector package currently only
// supports the EA Desktop app on Windows.
// -Generalisk, 02/01/26
#if PLATFORM_WINDOWS
using GameCollector.StoreHandlers.EADesktop;
using GameCollector.StoreHandlers.EADesktop.Crypto.Windows;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class EADesktopLibrary : Library<EADesktopGame, EADesktopHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.EADesktop;

    protected override EADesktopHandler GetHandler()
    {
        Console.WriteLine("Initializing EA Desktop Handler...");

        var fileSystem = FileSystem.Shared;
        var registry = WindowsRegistry.Shared;
        var hardwareInfoProvider = new HardwareInfoProvider();

        var handler = new EADesktopHandler(fileSystem, registry, hardwareInfoProvider);

        Console.WriteLine("EA Desktop Handler Initialized!");

        return handler;
    }

    public override EADesktopGame? Get(object id)
    {
        var gameid = EADesktopGameId.From(id.ToString());
        var game = handler.FindOneGameById(gameid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override EADesktopGame[] GetAll()
    {
        var games = new List<EADesktopGame>();

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
#endif
