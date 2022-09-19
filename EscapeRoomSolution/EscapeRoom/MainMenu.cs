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
    
        
        public void startGameMessage() {
            ConsoleKeyInfo userKeyInput;
            bool keyPressedRight = false;

            Console.WriteLine("Press Enter to Start");

            while (!keyPressedRight) {
                
                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Enter) { 
                    keyPressedRight = true;
                    Console.ResetColor();
                    Console.Clear();
                    StartGame();
                }
            }
           
        }
        
        

        public void StartGame()
        {
            bool userInputRight = false;
            Console.WriteLine("You are going to infiltrate SAE's headquarter.\nThis mission necessitates the use of a code name. What is it going to be?");
            Console.ForegroundColor = ConsoleColor.Green;
            

            while (!userInputRight) {
                Console.Write("Enter your code name: ");
                string playerName = Console.ReadLine();
                if (Regex.IsMatch(playerName, "[a-zA-Z]") == true && playerName.Length > 2)
                {
                    userInputRight = true;
                }
            }
            
            
        }
    }
}
