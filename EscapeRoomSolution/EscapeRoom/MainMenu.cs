using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Menu
    {
        string playerName = "";
        
        //Display game title screen
        public string StartGameMessage() {
            ConsoleKeyInfo userKeyInput;
            bool keyPressedRight = false;

            PrinInColor("Press Enter to Start", ConsoleColor.Yellow, true);

            while (!keyPressedRight) {
                
                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Enter) { 
                    keyPressedRight = true;
                    Console.Clear();
                    playerName = IntroGame();
                }
            }

            return playerName;
        }

        //Introduction saves player name into variable
        public string IntroGame()
        {
            bool userInputRight = false;
            int codeNameTries = 1;

            Console.WriteLine("You are going to infiltrate SAE's headquarter.\nThis mission necessitates the use of a code name. What is it going to be?");

            //promt player name saves into variabl
            while (!userInputRight) {
            PrinInColor("Enter your code name: ", ConsoleColor.Green, false);

                playerName = Console.ReadLine();

                if (Regex.IsMatch(playerName, "[a-zA-Z]") == true && playerName.Length > 2 && playerName.Length < 20 || codeNameTries == 3)
                {
                    userInputRight = true;
                } else {
                    codeNameTries++; 
                }
                
            }
            
            //after 3 tries set the value to "zero"
            if (codeNameTries == 3) {
                playerName = "Zero";
            }

            return playerName;
        }

        //method prints message in color
        public void PrinInColor(string message, ConsoleColor color, bool resetColor)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            if (resetColor)
            {
                Console.ResetColor();
            }
        }
    }
}
