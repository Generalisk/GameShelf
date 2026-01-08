using Raylib_cs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace GameShelf.Utilities;

internal static class ImageTools
{
    public static Texture2D LoadTexture(string path, int width = -1, int height = -1)
    {
        if (width <= 0 && height <= 0)
            return Raylib.LoadTexture(path);

        var img = SixLabors.ImageSharp.Image.Load(path);

        if (height <= 0) height = RecalculateHeight(img.Height, img.Width, width);
        else if (width <= 0) width = RecalculateWidth(img.Width, img.Height, height);

        img.Mutate(x => x.Resize(width, height));
        img.SaveAsPng("temp.png");
        img.Dispose();

        var output = Raylib.LoadTexture("temp.png");
        File.Delete("temp.png");

        return output;
    }

    private static int RecalculateWidth(int width, int oldHeight, int newHeight)
    {
        float scale = (1f / oldHeight) * newHeight;
        return Convert.ToInt32(width * scale);
    }

    private static int RecalculateHeight(int height, int oldWidth, int newWidth)
    {
        float scale = (1f / oldWidth) * newWidth;
        return Convert.ToInt32(height * scale);
    }
}
