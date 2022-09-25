using System;

namespace EscapeRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ASCIISign titleSign = new ASCIISign();
            Menu mainMenu = new Menu();
            Player player = new Player();



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(titleSign.title);

            player.codeName = mainMenu.StartGameMessage();

            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Agent " + player.codeName + ", welcome aboard! We look forward to working with you. \n\nLet's go back to business. According to our sources, SAE is planning a secret weapon that will pose a danger to our \nPresident Joe Mama. One of our spies was dispatched to gather intel from the SAE's headquarters, but we lost contact \nwith him after a time. \n\nNow it's your turn to track him down and figure out what's going on at their headquarters, but first we need an \nestimate of the size of one of their buildings.");
            
            
        }
    }
}
