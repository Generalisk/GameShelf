using ImGuiNET;

namespace GameShelf.Windows.Static;

internal abstract class StaticWindow : IDisposable
{
    public abstract string Title { get; }

    public ImGuiWindowFlags Flags { get; } =
        ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize |
        ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus;


    protected virtual void Init() { }
    public abstract void Draw();
    protected virtual void Shutdown() { }

    public StaticWindow() => Init();

    public void Dispose() => Shutdown();
}
