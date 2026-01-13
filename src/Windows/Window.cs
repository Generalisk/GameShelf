using ImGuiNET;

namespace GameShelf.Windows;

internal abstract class Window : IDisposable
{
    public abstract string Title { get; }
    public abstract ImGuiWindowFlags Flags { get; }

    public virtual void Init() { }
    public abstract void Draw();
    public virtual void Shutdown() { }

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
