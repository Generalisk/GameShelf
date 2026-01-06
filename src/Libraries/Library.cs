using GameFinder.Common;

namespace GameShelf.Libraries;

internal abstract class Library<TGame, THandler>
    where TGame : GameData where THandler : AHandler
{
    protected abstract LibrarySource Source { get; }

    protected THandler handler;

    public Library() => handler = GetHandler();

    protected abstract THandler GetHandler();

    public abstract TGame? Get(object id);
    public abstract TGame[] GetAll();
    public abstract void Refresh();

    public void Clear()
    {
        var previousLength = Games.Length;

        if (previousLength <= 0) return;

        // Simply using RemoveAll wouldn't dispose/unload them properly,
        // so we get a list of all the GameInfo's we want to remove
        // and manually unload them one-by-one.
        // -Generalisk, 31/12/25
        var gamez = Games.Where(x => x.Source == Source);

        if (gamez.Count() <= 0) return;

        foreach (var game in gamez)
            game.Dispose();

        var removedCount = Games.Length - previousLength;
        var libName = Enum.GetName(Source);
        Console.WriteLine("Successfully unloaded {0} {1} games from game index!", removedCount, libName);
    }
}
