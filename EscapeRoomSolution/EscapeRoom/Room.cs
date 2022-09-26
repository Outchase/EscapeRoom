﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Room
    {
        public int width;
        public int height;

        public string sideWall = "║";
        public string uppLowerWall = "═";

        public static int VerifyRoomSize(string userInput, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int numericValue = 0;
            bool isNumber = false;

            while (!isNumber)
            {
                Console.Write("Enter the " + message + " of the building (minimum 15): ");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out numericValue) && numericValue >= 15)
                {
                    isNumber = true;
                }
            }
            Console.ResetColor();
            return numericValue;
        }

        public void ConfirmSize()
        {
            string userInput = "";
            bool userChoice = false;
            
            while (!userChoice)
            {
                width = VerifyRoomSize(userInput, "width");
                height = VerifyRoomSize(userInput, "height");

                Console.WriteLine("Width: " + width);
                Console.WriteLine("Height: " + height);

                GenerateRoom(width, height, 0,0, "");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Are you sure with the size? [Y/n] ");

                ConsoleKeyInfo userKeyInput;
                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Y)
                {
                    userChoice = true;
                }
                Console.Clear();
            }
            Console.ResetColor();

            
        }

        public void GenerateRoom(int roomWidth, int roomHeight, int playerY, int playerX, string playerSprite) {

            for (int i = 1; i <= roomHeight; i++)
            {
                for (int j = 1; j <= roomWidth; j++)
                {
                    if (i == 1 || i == roomHeight || j == 1 || j == roomWidth)
                    {
                        if (i == 1 && j == 1)
                        {
                            Console.Write("╔");
                        }
                        else if (i == 1 && j == roomWidth)
                        {
                            Console.Write("╗");
                        }
                        else if (i == roomHeight && j == 1)
                        {
                            Console.Write("╚");
                        }
                        else if (i == roomHeight && j == roomWidth)
                        {
                            Console.Write("╝");
                        }
                        else if (i == 1 && j <= roomWidth || i == roomHeight && j <= roomWidth)
                        {
                            Console.Write(uppLowerWall);
                        }
                        else
                        {
                            Console.Write(sideWall);
                        }
                    }
                    else if (i == playerY && j == playerY)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(playerSprite);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
