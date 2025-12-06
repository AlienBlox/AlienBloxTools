using System.Diagnostics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AlienBloxTools.Utilities
{
    public static class TmodExtractorUtility
    {
        /// <summary>
        /// Extracts an entire tMod file
        /// </summary>
        /// <param name="FileLocation">The location of the tMod file</param>
        /// <returns>The task.</returns>
        public static async Task ExtractTmodFile(string FileLocation)
        {
            if (!File.Exists($"{InitialiseUtilities.EXESaves}\\tModUnpacker"))
            {
                InitialiseUtilities.ExtractTMODUnpacker();

                return;
            }

            if (File.Exists(FileLocation))
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = $"{InitialiseUtilities.EXESaves}\\tModUnpacker",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    Arguments = FileLocation
                };

                using Process process = new()
                { StartInfo = startInfo, EnableRaisingEvents = true };
                process.Start();

                // Async read of output
                string output = await process.StandardOutput.ReadToEndAsync();

                // Wait asynchronously for exit
                await Task.Run(() => process.WaitForExit());

                Console.WriteLine("Output:");
                Console.WriteLine(output);
            }
        }
    }
}