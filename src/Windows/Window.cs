using ImGuiNET;

namespace GameShelf.Windows;

internal abstract class Window : IDisposable
{
    public abstract string Title { get; }
    public abstract ImGuiWindowFlags Flags { get; }

    protected virtual void Init() { }
    public abstract void Draw();
    protected virtual void Shutdown() { }

    public Window()
    {
        windows.Add(this);

        Init();
    }

    public void Dispose()
    {
        Shutdown();

        windows.Remove(this);
    }
}
