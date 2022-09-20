using System;

namespace EscapeRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIISign titleSign = new ASCIISign();
            Menu mainMenu = new Menu();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(titleSign.title);

            mainMenu.startGameMessage();

            Console.ResetColor();
            Console.WriteLine("Game start");
            
        }
    }
}
