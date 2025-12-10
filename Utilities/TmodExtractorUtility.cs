using System.Diagnostics;

namespace AlienBloxTools.Utilities
{
    public static class TmodExtractorUtility
    {
        /// <summary>
        /// Extracts a tMod file including the code.
        /// </summary>
        /// <param name="FileLocation">The location to extract to.</param>
        /// <param name="FileName">The DLL file of the mod to extract</param>
        /// <param name="UtilityToExtractWith">The utility to extract the tMod file with</param>
        /// <returns>The task lol.</returns>
        public static async Task ExtractMod(string FileLocation, string FileName, string UtilityToExtractWith)
        {
            if (!File.Exists($"{FileLocation}.tmod"))
            {
                return;
            }

            await ExtractTmodFile(FileLocation, UtilityToExtractWith);

            DLLExtract.DecompileDllToFolder($"{FileLocation}\\{FileName}.dll", FileLocation);
        }

        /// <summary>
        /// Extracts an entire tMod file
        /// </summary>
        /// <param name="FileLocation">The location of the tMod file (No file extension)</param>
        /// <param name="UtilityToExtractWith">The utility to extract the tModLoader mod with</param>
        /// <returns>The task.</returns>
        public static async Task ExtractTmodFile(string FileLocation, string UtilityToExtractWith)
        {
            if (!File.Exists(UtilityToExtractWith))
            {
                InitialiseUtilities.ExtractTMODUnpacker();

                return;
            }

            if (File.Exists(FileLocation))
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = UtilityToExtractWith,
                    UseShellExecute = true,
                    RedirectStandardOutput = true,
                    Arguments = $"{FileLocation}.tmod"
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