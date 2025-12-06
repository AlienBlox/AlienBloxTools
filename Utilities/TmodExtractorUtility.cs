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
        /// <returns>The task lol.</returns>
        public static async Task ExtractMod(string FileLocation, string FileName)
        {
            if (!File.Exists($"{FileLocation}.tmod"))
            {
                return;
            }

            await ExtractTmodFile(FileLocation);

            DLLExtract.DecompileDllToFolder($"{FileLocation}\\{FileName}.dll", FileLocation);
        }

        /// <summary>
        /// Extracts an entire tMod file
        /// </summary>
        /// <param name="FileLocation">The location of the tMod file (No file extension)</param>
        /// <returns>The task.</returns>
        public static async Task ExtractTmodFile(string FileLocation)
        {
            if (!File.Exists($"{InitialiseUtilities.EXESaves}\\tModUnpacker.exe"))
            {
                InitialiseUtilities.ExtractTMODUnpacker();

                return;
            }

            if (File.Exists(FileLocation))
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = $"{InitialiseUtilities.EXESaves}\\tModUnpacker.exe",
                    UseShellExecute = false,
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