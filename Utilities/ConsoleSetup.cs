using Terraria;

namespace AlienBloxTools.Utilities
{
    public class ConsoleSetup
    {
        public static void RedirectConsole(string Msg)
        {
            using StreamWriter sw = new($"{Main.SavePath}\\AlienBloxTools\\Logs.txt");
            Console.WriteLine();
            Console.WriteLine(Msg);
        }
    }
}