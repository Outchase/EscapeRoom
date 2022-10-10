﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Room
    {
        Menu mainMenu = new Menu();
        public int width;
        public int height;


        public string sideWall = "║";
        public string uppLowerWall = "═";

        //verify input values of room size to generate
        public static int VerifyRoomSize(string userInput, string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int numericValue = 0;
            bool isNumber = false;
            
            //min and max inputs and parse to initger
            while (!isNumber)
            {
                Console.Write("Enter the " + message + " of the building (minimum 15 - maximum 230): ");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out numericValue) && numericValue >= 15 && numericValue <= 230)
                {
                    isNumber = true;
                }
            }

            Console.ResetColor();
            return numericValue;
        }

        //set and change the room size before confirm
        public void ConfirmSize(Player player, Key key, Door door, Menu mainMenu)
        {
            string userInput = "";
            bool didConfirm = false;

            while (!didConfirm)
            {
                //prompt width & height and display room size
                width = VerifyRoomSize(userInput, "width");
                height = VerifyRoomSize(userInput, "height");
                Console.WriteLine("Width: " + width);
                Console.WriteLine("Height: " + height);
                GenerateRoom(player, key, door);

                mainMenu.PrinInColor("Are you sure with this size? [Y/n] ", ConsoleColor.Green, true);

                ConsoleKeyInfo userKeyInput;
                bool keyPressedRight = false;

                while (!keyPressedRight)
                {

                    userKeyInput = Console.ReadKey(true);

                    if (userKeyInput.Key == ConsoleKey.Y)
                    {
                        keyPressedRight = true;
                        didConfirm = true;
                        Console.Clear();
                    }
                    else if (userKeyInput.Key == ConsoleKey.N)
                    {
                        keyPressedRight = true;
                        Console.Clear();
                    }
                }
            }
        }

        //generate the room and displays it //change to array
        public void GenerateRoom(Player player, Key key, Door door)
        {

            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= width; j++)
                {
                    if (i == 1 || i == height || j == 1 || j == width || i == door.position[1] && j == door.position[0])
                    {
                        if (i == 1 && j == 1)
                        {
                            Console.Write("╔");
                        }
                        else if (i == 1 && j == width)
                        {
                            Console.Write("╗");
                        }
                        else if (i == height && j == 1)
                        {
                            Console.Write("╚");
                        }
                        else if (i == height && j == width)
                        {
                            Console.Write("╝");
                        }
                        else if (i == 1 && j <= width || i == height && j <= width)
                        {
                            if (i == door.position[1] && j == door.position[0] && key.isCollect)
                            {
                                mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true);
                            }
                            else
                            {
                                Console.Write(uppLowerWall);
                            }
                        }
                        else
                        {
                            if (i == door.position[1] && j == door.position[0] && key.isCollect)
                            {
                                mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true);
                            }
                            else
                            {
                                Console.Write(sideWall);
                            }
                        }
                    }   //generate the wall around
                    else if (i == player.position[1] && j == player.position[0] || i == key.position[1] && j == key.position[0] )
                    { //checks if the loop passes on of the items/player cordinations 
                        if (i == player.position[1] && j == player.position[0])      //add the object to the room
                        {
                            mainMenu.PrinInColor(player.sprite, ConsoleColor.Red, true);
                        }
                        else
                        {
                            if (key.isCollect)
                            {
                                Console.Write(" ");
                            }
                            else
                            {
                                mainMenu.PrinInColor(key.sprite, ConsoleColor.Green, true);
                            }
                        }
                        /*else
                        {
                            if (key.isCollect)
                            {
                                mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true);
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }*/
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
