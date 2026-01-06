using GameCollector.StoreHandlers.Steam;
using GameCollector.StoreHandlers.Steam.Models.ValueTypes;
using GameFinder.RegistryUtils;
using NexusMods.Paths;

namespace GameShelf.Libraries;

internal class SteamLibrary : Library<SteamGame, SteamHandler>
{
    protected override LibrarySource Source { get; } = LibrarySource.Steam;

    private const string CoverArtPath = "{0}/appcache/librarycache/{1}/library_600x900.jpg";

    private string steamPath = "";

    protected override SteamHandler GetHandler()
    {
        Console.WriteLine("Initializing Steam Handler...");

        var fileSystem = FileSystem.Shared;
#if PLATFORM_WINDOWS
        var registry = WindowsRegistry.Shared;
#else
        IRegistry? registry = null;
#endif

        var handler = new SteamHandler(fileSystem, registry);

        steamPath = handler.FindClient().GetFullPath();

        Console.WriteLine("Steam Handler Initialized!");

        return handler;
    }

    public override SteamGame? Get(object id)
    {
        var appid = AppId.From(Convert.ToUInt32(id));
        var game = handler.FindOneGameById(appid, out var errors);

        foreach (var error in errors)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        return game;
    }

    public override SteamGame[] GetAll()
    {
        var games = new List<SteamGame>();

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
            var coverArt = string.Format(CoverArtPath, steamPath, game.AppId.Value);
            var launchPath = "";

            if (game.Icon != default)
                icon = game.Icon.GetFullPath();

            if (!File.Exists(coverArt))
                coverArt = "";

            if (game.Launch != default)
                launchPath = game.Launch.GetFullPath();

            new GameInfo()
            {
                Name = game.GameName,
                Source = Source,
                IconPath = icon,
                CoverArtPath = coverArt,
                LaunchPath = launchPath,
                LaunchArgs = game.LaunchArgs,
            };
        }
    }
}
