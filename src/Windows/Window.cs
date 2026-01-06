using ImGuiNET;

namespace GameShelf.Windows;

internal abstract class Window : IDisposable
{
    public abstract string Title { get; }
    public abstract ImGuiWindowFlags Flags { get; }

    public abstract void Draw();

    public Window() { windows.Add(this); }

    public void Dispose() { windows.Remove(this); }
}
