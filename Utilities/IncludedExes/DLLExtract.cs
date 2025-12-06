using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;

namespace AlienBloxTools.Utilities.IncludedExes
{
    public static class DLLExtract
    {
        /// <summary>
        /// Decompile a DLL and export all top-level types as .cs files.
        /// </summary>
        /// <param name="dllPath">Path to the DLL to decompile.</param>
        /// <param name="outputFolder">Folder to save the .cs files.</param>
        public static void DecompileDllToFolder(string dllPath, string outputFolder)
        {
            if (!File.Exists(dllPath))
                throw new FileNotFoundException("DLL not found", dllPath);

            Directory.CreateDirectory(outputFolder);

            var decompiler = new CSharpDecompiler(dllPath, new DecompilerSettings());

            // Loop through all top-level type definitions
            foreach (var type in decompiler.TypeSystem.MainModule.TypeDefinitions)
            {
                if (type.FullName == "<Module>") continue;

                // Decompile using FullTypeName
                string code = decompiler.DecompileTypeAsString(type.FullTypeName);

                // Preserve namespace folder structure
                string namespacePath = type.Namespace.Replace('.', Path.DirectorySeparatorChar);
                string typeFolder = Path.Combine(outputFolder, namespacePath);
                Directory.CreateDirectory(typeFolder);

                string filePath = Path.Combine(typeFolder, type.Name + ".cs");
                File.WriteAllText(filePath, code);

                Console.WriteLine($"Exported: {filePath}");
            }
        }
    }
}
