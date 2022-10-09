﻿using System;
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
        public string StartGameMessage() {
            ConsoleKeyInfo userKeyInput;
            bool keyPressedRight = false;

            PrinInColor("Press Enter to Start", ConsoleColor.Yellow, true);

            while (!keyPressedRight) {
                
                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Enter) { 
                    keyPressedRight = true;
                    Console.Clear();
                    playerName = StartGame();
                }
            }

            return playerName;
        }

        public string StartGame()
        {
            bool userInputRight = false;
            int codeNameTries = 1;

            Console.WriteLine("You are going to infiltrate SAE's headquarter.\nThis mission necessitates the use of a code name. What is it going to be?");

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
            
            if (codeNameTries == 3) {
                playerName = "Zero";
            }

            return playerName;
        }

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
