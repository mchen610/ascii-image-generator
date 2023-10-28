using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            string blackToWhite = ".:-=+*#%@";
            string luminance = blackToWhite;
            using Image<Rgba32> image = Image.Load<Rgba32>(args[0]);

            int max = 150;
            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = max;
                newHeight = (int)((double)image.Height / image.Width * max);
            }
            else
            {
                newHeight = max;
                newWidth = (int)((double)image.Width / image.Height * max);
            }

            // Resize the image
            image.Mutate(ctx => ctx.Resize(newWidth, newHeight));

            string asciiImage = "";
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    double pixelBrightness = getBrightness(pixel);
                    char asciiChar = luminance[(int)(pixelBrightness * luminance.Length)];
                    asciiImage += $"{asciiChar}{asciiChar}";

                }
                asciiImage += "\n";
            }
            Console.WriteLine(asciiImage);

        }
        Console.ReadLine();
    }

    public static double getBrightness(Rgba32 pixel)
    {
        return (0.299 * pixel.R + 0.586 * pixel.G + 0.114 * pixel.B) / 255;
    }
}
