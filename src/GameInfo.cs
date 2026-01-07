using GameShelf.Libraries;
using ImageMagick;
using Raylib_cs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
#if PLATFORM_WINDOWS
using System.Drawing;
using System.Drawing.Imaging;
#endif

namespace GameShelf;

internal class GameInfo : IDisposable
{
    public string Name { get; init; } = "";
    public LibrarySource Source { get; init; } = LibrarySource.Unknown;

    public string IconPath { get; init; } = "";
    public Texture2D? Icon { get => icon; }
    private Texture2D? icon = null;

    public string CoverArtPath { get; init; } = "";
    public Texture2D? CoverArt { get => coverArt; }
    private Texture2D? coverArt = null;

    public string LaunchURL { get; init; } = "";
    public string LaunchPath { get; init; } = "";
    public string LaunchArgs { get; init; } = "";

    public GameInfo()
    {
        games.Add(this);

        LoadAll();
    }

    public void Dispose()
    {
        UnloadAll();

        games.Remove(this);
    }

    public void LoadAll()
    {
        LoadIcon();
        LoadCoverArt();
    }

    public void UnloadAll()
    {
        UnloadIcon();
        UnloadCoverArt();
    }

    public void LoadIcon()
    {
        LoadQueue.Add(() =>
        {
            if (string.IsNullOrWhiteSpace(IconPath))
                return;

            if (Icon != null)
                UnloadIcon();

            switch (new FileInfo(IconPath).Extension)
            {
                case ".ico":
                    var image = new MagickImage(IconPath);
                    image.Format = MagickFormat.Png;
                    image.Resize(20, 20);
                    image.Write("temp.png");
                    image.Dispose();
                    break;
#if PLATFORM_WINDOWS
                case ".exe":
                    var ico = System.Drawing.Icon.ExtractAssociatedIcon(IconPath);
                    var bitmap = ico.ToBitmap();
                    bitmap = new Bitmap(bitmap, new System.Drawing.Size(20, 20));
                    bitmap.Save("temp.png", ImageFormat.Png);
                    bitmap.Dispose();
                    ico.Dispose();
                    break;
#endif
                default:
                    var img = SixLabors.ImageSharp.Image.Load(IconPath);
                    img.Mutate(x => x.Resize(20, 20));
                    img.SaveAsPng("temp.png");
                    img.Dispose();
                    break;
            }

            icon = Raylib.LoadTexture("temp.png");
            File.Delete("temp.png");
        });
    }

    public void UnloadIcon()
    {
        if (Icon == null)
            return;

        Raylib.UnloadTexture(Icon.Value);
        icon = null;
    }

    public void LoadCoverArt()
    {
        LoadQueue.Add(() =>
        {
            if (string.IsNullOrWhiteSpace(CoverArtPath))
                return;

            if (CoverArt != null)
                UnloadCoverArt();

            var img = SixLabors.ImageSharp.Image.Load(CoverArtPath);

            int windowWidth = Raylib.GetRenderWidth() - Convert.ToInt32(Raylib.GetRenderWidth() / 4.5f);
            int columns = windowWidth / 200;
            int newWidth = Convert.ToInt32((windowWidth - (columns * 18)) / columns);
            float scale = (1f / img.Width) * newWidth;
            int newHeight = Convert.ToInt32(img.Height * scale);

            img.Mutate(x => x.Resize(newWidth, newHeight));
            img.SaveAsJpeg("temp.jpg");
            img.Dispose();

            coverArt = Raylib.LoadTexture("temp.jpg");
            File.Delete("temp.jpg");
        });
    }

    public void UnloadCoverArt()
    {
        if (CoverArt == null)
            return;

        Raylib.UnloadTexture(CoverArt.Value);
        coverArt = null;
    }
}
