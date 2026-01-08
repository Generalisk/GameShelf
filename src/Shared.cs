global using static GameShelf.Shared;

using GameShelf.Libraries;
using GameShelf.Utilities;
using GameShelf.Windows;
using Raylib_cs;

namespace GameShelf;

internal static class Shared
{
    public static string SavePath
    {
        get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/GameShelf";
    }

    public static GameInfo[] Games { get => games.ToArray(); }
    internal static List<GameInfo> games = new List<GameInfo>();
    public static GameInfo? SelectedGame { get; set; } = null;

    public static Window[] Windows { get => windows.ToArray(); }
    internal static List<Window> windows = new List<Window>();
    internal static GameListWindow GameListWindow { get; set; }
    internal static MainWindow MainWindow { get; set; }

    public static Texture2D MissingIcon { get; internal set; }
    public static Texture2D MissingCoverArt { get; internal set; }

    internal static bool Close { get; set; } = false;

    public static void RefreshAll()
    {
        new LocalLibrary().Refresh();

        new SteamLibrary().Refresh();
        new ItchLibrary().Refresh();
        new GOGLibrary().Refresh();
        new GameJoltLibrary().Refresh();
        new EGSLibrary().Refresh();
#if PLATFORM_WINDOWS
        new XboxLibrary().Refresh();
#endif
        new OriginLibrary().Refresh();
#if PLATFORM_WINDOWS
        new EADesktopLibrary().Refresh();
#endif
        new BattleNetLibrary().Refresh();
        //new BethesdaLibrary().Refresh();
        new UbisoftLibrary().Refresh();
        new RockstarLibrary().Refresh();
        new RiotLibrary().Refresh();
        new AmazonLibrary().Refresh();
        new OculusLibrary().Refresh();
        //new HeroicGOGLibrary().Refresh();
        //new DolphinLibrary().Refresh();
        new LegacyLibrary().Refresh();
        new IGClientLibrary().Refresh();
        new ParadoxLibrary().Refresh();
        new PlariumLibrary().Refresh();
        //new MAMELibrary().Refresh();
        new ArcLibrary().Refresh();
        new BigFishLibrary().Refresh();
        new HumbleLibrary().Refresh();
        new RobotCacheLibrary().Refresh();
        new WarGamingLibrary().Refresh();

        games = Games.OrderBy(x => x.Name).ToList();
    }

    public static void ReloadMissingIcon()
    {
        LoadQueue.Add(() =>
        {
            Raylib.UnloadTexture(MissingIcon);

            ImageTools.LoadTexture("Resources/MissingIcon.png", 20, 20);
        });
    }

    public static void ReloadMissingCoverArt()
    {
        LoadQueue.Add(() =>
        {
            Raylib.UnloadTexture(MissingCoverArt);

            int windowWidth = Raylib.GetRenderWidth() - Convert.ToInt32(Raylib.GetRenderWidth() / 4.5f);
            int columns = windowWidth / 200;
            int newWidth = Convert.ToInt32((windowWidth - (columns * 18)) / columns);

            MissingCoverArt = ImageTools.LoadTexture("Resources/MissingCoverArt.png", newWidth);
        });
    }
}
