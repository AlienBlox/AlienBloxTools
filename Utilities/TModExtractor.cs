using System;
using System.IO;
using System.IO.Compression;
using Terraria;

namespace AlienBloxTools.Utilities
{
    /// <summary>
    /// An utility used to extract tMod file data.
    /// </summary>
    public static class TModExtractor
    {
        public static void Extract(string FileLocation, string ExtractionFolderName)
        {
            string tmodFilePath = FileLocation;
            string extractFolder = $"{Main.SavePath}\\ExtractedMods\\{ExtractionFolderName}";

            Directory.CreateDirectory(extractFolder);

            // Open the .tMod file
            using (FileStream tmodStream = new(tmodFilePath, FileMode.Open, FileAccess.Read))
            {
                using BinaryReader reader = new(tmodStream);
                // Skip the tMod header (first 3 bytes: "tMod" + version info)
                byte[] header = reader.ReadBytes(4); // tMod files often start with "tMod"

                // The rest of the file is zlib-compressed
                using DeflateStream deflateStream = new(tmodStream, CompressionMode.Decompress);
                using FileStream outFile = new(Path.Combine(extractFolder, "decompressed.mod"), FileMode.Create);
                deflateStream.CopyTo(outFile);
            }

            Console.WriteLine("Decompression complete!");
        }
    }
}