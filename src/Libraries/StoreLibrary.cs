using GameFinder.Common;

namespace GameShelf.Libraries;

internal abstract class StoreLibrary<TGame, THandler> : Library
    where TGame : GameData where THandler : AHandler
{
    protected THandler handler;

    public StoreLibrary() => handler = GetHandler();

    protected abstract THandler GetHandler();

    public abstract TGame? Get(object id);
    public abstract TGame[] GetAll();

    public override void Load() =>
        throw new NotImplementedException();
    public override void Save() =>
        throw new NotImplementedException();
}
