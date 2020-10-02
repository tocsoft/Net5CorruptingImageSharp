using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageSharpResizeCorruptionBug
{
    class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.OpenRead(@"source_small.jpg");
            using var image = new JpegDecoder().Decode<Rgba32>(Configuration.Default, fs);
            using SHA256 hashAlgorithm = SHA256.Create();

            if(image.TryGetSinglePixelSpan(out var span))
            {
                var dataIn = System.Runtime.InteropServices.MemoryMarshal.AsBytes(span);
                
                // Convert the input string to a byte array and compute the hash.
                byte[] data = hashAlgorithm.ComputeHash(dataIn.ToArray());

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                Console.WriteLine(sBuilder.ToString());
            }
            else 
            {
                Console.WriteLine("failed to load single span");
            }
        }
    }
}
