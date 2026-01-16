using ImGuiNET;
using System.Numerics;

namespace GameShelf.Windows;

internal abstract class Window : IDisposable
{
    public abstract string Title { get; }
    public abstract ImGuiWindowFlags Flags { get; }

    public virtual Vector2 MinSize { get; } = Vector2.Zero;
    public virtual Vector2 MaxSize { get; } = new Vector2(int.MaxValue);

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
