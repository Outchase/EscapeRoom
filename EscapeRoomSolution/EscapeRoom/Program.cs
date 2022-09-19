using System;

namespace EscapeRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIISign titleSign = new ASCIISign();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(titleSign.title);
            Menu mainMenu = new Menu();
            mainMenu.startGameMessage();
            Console.WriteLine("Game start");
            
        }
    }
}
