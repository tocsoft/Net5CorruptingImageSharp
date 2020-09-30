using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageSharpResizeCorruptionBug
{
     class Program
    {
        static async Task Main(string[] args)
        {
            var outputDirectory = ".";
            if (args.Any())
            {
                outputDirectory = args[0];
            }

            var imageProcessor = new ImageProcessor();

            string outputFileName = Path.Join(outputDirectory, "small.jpg");
            string inputFile = @"source_small.jpg";
            await ResizeTestAsync(imageProcessor, outputFileName, inputFile);

             outputFileName = Path.Join(outputDirectory, "garden.jpg");
             inputFile = @"source.jpg";
            await ResizeTestAsync(imageProcessor, outputFileName, inputFile);

            Console.WriteLine("done");
        }

        private static async Task ResizeTestAsync(ImageProcessor imageProcessor, string outputFileName, string inputFile)
        {
            using Stream inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
            using (MemoryStream lowResStream = imageProcessor.ResizeTo(inputStream, ImageProcessor.LowResWidth))
            {
                using (Stream outputStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                {
                    await lowResStream.CopyToAsync(outputStream);
                }
            }
        }
    }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         var outputDirectory = ".";
    //         if (args.Any())
    //         {
    //             outputDirectory = args[0];
    //         }

    //       //  Directory.CreateDirectory(outputDirectory);
    //         using var image = Image.Load("source.jpg");

    //         image.Mutate(x => x.Resize(width: 500, 0));

    //         var path = Path.Combine(outputDirectory, "result.jpg");
    //         image.Save(path);
    //         Console.WriteLine(path);
    //     }
    // }
    

    public class ImageProcessor
    {
        public const string LowResExtension = ".low";
        public const int LowResWidth = 700;
        public const int DefaultQuality = 90;

        public MemoryStream ResizeTo(Stream input, int width)
        {
            var output = new MemoryStream();
            input.Seek(0, SeekOrigin.Begin);
            using Image image = Image.Load(input);
            image.Mutate(x => x.Resize(width: width, 0));
            image.SaveAsJpeg(output);
            output.Seek(0, SeekOrigin.Begin);
            input.Seek(0, SeekOrigin.Begin);
            return output;
        }

        public Stream ReCompress(Stream input, int quality = DefaultQuality)
        {
            var output = new MemoryStream();
            input.Seek(0, SeekOrigin.Begin);
            using Image image = Image.Load(input);
            image.Mutate(x => x.AutoOrient());
            image.SaveAsJpeg(output, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = quality });
            output.Seek(0, SeekOrigin.Begin);
            input.Seek(0, SeekOrigin.Begin);
            return output;
        }

        /// <summary>
        /// Rotate image so it always is displayed correctly. Workaround for client not doing this.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Stream AutoOrient(Stream input)
        {
            input.Seek(0, SeekOrigin.Begin);
            using Image image = Image.Load(input);
            IExifValue exifOrientation = image.Metadata?.ExifProfile?.GetValue(ExifTag.Orientation);
            if (exifOrientation?.GetValue() == null || exifOrientation.GetValue().ToString() == "1")
            {
                input.Seek(0, SeekOrigin.Begin);
                return input;
            }
            var output = new MemoryStream();
            image.Mutate(x => x.AutoOrient());
            image.SaveAsJpeg(output, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = DefaultQuality });
            output.Seek(0, SeekOrigin.Begin);
            input.Seek(0, SeekOrigin.Begin);
            return output;
        }
    }
}
