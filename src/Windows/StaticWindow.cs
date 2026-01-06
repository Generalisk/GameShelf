using ImGuiNET;

namespace GameShelf.Windows;

internal abstract class StaticWindow : Window
{
    public override ImGuiWindowFlags Flags { get; } =
        ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize |
        ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoBringToFrontOnFocus;

    public StaticWindow() { }
}
