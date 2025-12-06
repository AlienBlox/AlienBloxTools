using Terraria;

namespace AlienBloxTools.Utilities
{
    public class ConsoleSetup
    {
        public static void RedirectConsole()
        {
            using StreamWriter sw = new($"{Main.SavePath}\\AlienBloxTools\\Logs.txt");
            Console.WriteLine("Redirecting console...");
            Console.SetOut(sw);
            Console.WriteLine("Redirected console");
        }
    }
}