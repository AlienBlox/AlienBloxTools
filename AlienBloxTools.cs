using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.IO;

namespace AlienBloxTools
{
    /// <summary>
    /// The official tools made by AlienBlox
    /// </summary>
    public static class AlienBloxTools
    {
        /// <summary>
        /// Gets the connected TMod file of a tModLoader mod
        /// </summary>
        /// <param name="mod">The mod to query</param>
        /// <returns>The tModLoader mod file</returns>
        public static TmodFile GetTmodFile(this Mod mod)
        {
            Type ModType = mod.GetType();
            PropertyInfo? Info = ModType.GetProperty("File", BindingFlags.Instance | BindingFlags.NonPublic);

            if (Info != null)
            {
                object? Output = Info.GetValue(mod);

                if (Output != null && Output is TmodFile file)
                {
                    return file;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a tModLoader mod's assembly, good for decompiling the mod
        /// </summary>
        /// <param name="mod">The mod to query</param>
        /// <returns>The assembly byte array</returns>
        public static byte[] GetModAssembly(this Mod mod)
        {
            return mod.GetTmodFile().GetModAssembly();
        }

        /// <summary>
        /// Gets a tModLoader mod's PDB, good for decompiling the mod
        /// </summary>
        /// <param name="mod">The mod to query</param>
        /// <returns>The PDB byte array</returns>
        public static byte[] GetModPDB(this Mod mod)
        {
            return mod.GetTmodFile().GetModPdb();
        }

        /// <summary>
        /// Gets the mod icon of a mod as a byte array
        /// </summary>
        /// <param name="mod">The mod to get the icon from.</param>
        /// <returns>The mod icon</returns>
        public static byte[] GetModIcon(this Mod mod)
        {
            byte[] Icon = [];

            if (mod.GetTmodFile().GetBytes("icon.png") != null)
            {
                Icon = mod.GetTmodFile().GetBytes("icon.png");
            }

            return Icon;
        }

        public static Texture2D GetModTexture(this Mod mod)
        {
            if (mod.GetModIcon() != null)
            {
                return Texture2D.FromStream(Main.graphics.GraphicsDevice, mod.GetModIcon().ToMemoryStream());
            }

            return null;
        }
    }
}