namespace GameShelf.Libraries;

public enum LibrarySource
{
    Unknown = 0,
    Local = 1,
    Steam = 2,
    Itch = 3,
    GOG = 4,
    GameJolt = 5,
    EGS = 6,
#if PLATFORM_WINDOWS
    Xbox = 7,
#endif
    Origin = 8,
#if PLATFORM_WINDOWS
    EADesktop = 9,
#endif
    BattleNet = 10,
    [Obsolete] Bethesda = 11,
    Ubisoft = 12,
    Rockstar = 13,
    Riot = 14,
    Amazon = 15,
    Oculus = 16,
    //HeroicGOG = 17,
    //Dolphin = 18,
    Legacy = 19,
    IGClient = 20,
    Paradox = 21,
    Plarium = 22,
    //MAME = 23,
    Arc = 24,
    BigFish = 25,
    Humble = 26,
    RobotCache = 27,
    WarGaming = 28,
}
