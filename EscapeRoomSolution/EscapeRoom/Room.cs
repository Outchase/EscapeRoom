using EscapeRoom.Properties;
using System;
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

        public string[,] roomArray;


        public string sideWall = "║";
        public string uppLowerWall = "═";

        //verify input values of room size to generate
        public static int VerifyRoomSize(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int numericValue = 0;
            bool isNumber = false;
            
            //min and max inputs and parse to initger
            while (!isNumber)
            {
                Console.Write(Resources.verifyRoomEnter + message + Resources.verifyRoomMinMax);
                string userInput = Console.ReadLine();

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
            bool didConfirm = false;

            while (!didConfirm)
            {
                //prompt width & height, generate and display the room size to confirm
                width = VerifyRoomSize(Resources.confirmRoomWidth);
                height = VerifyRoomSize(Resources.confirmRoomHeight);
                Console.WriteLine(Resources.confirmRoomWidth +" "+ width);
                Console.WriteLine(Resources.confirmRoomHeight +" "+ height);

                roomArray = GenerateRoom(player, key, door);
                DrawBoard(roomArray, player, key, door, mainMenu, false);



                mainMenu.PrinInColor(Resources.confirmRoomSizeMessage, ConsoleColor.Green, true);

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

        //generate the room and saves into array //change to array
        public string[,] GenerateRoom(Player player, Key key, Door door)
        {
            int tempHeight = height - 1;
            int tempWidth = width - 1;

            roomArray = new string[height,width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //generate the wall and corner
                    if (i == 0 || i == tempHeight || j == 0 || j == tempWidth || i == door.position[1] && j == door.position[0])
                    {
                        if (i == 0 && j == 0)
                        {
                            roomArray[i, j] = "╔";
                        }
                        else if (i == 0 && j == tempWidth)
                        {
                            roomArray[i, j] = "╗";
                        }
                        else if (i == tempHeight && j == 0)
                        {
                            roomArray[i, j] = "╚";
                        }
                        else if (i == tempHeight && j == tempWidth)
                        {
                            roomArray[i, j] = "╝";
                        }
                        else if (i == 0 && j <= tempWidth || i == tempHeight && j <= tempWidth)
                        {
                            if (i == door.position[1] && j == door.position[0] && key.isCollect)
                            {
                                roomArray[i, j] = door.sprite;
                            }
                            else
                            {
                                roomArray[i, j] = uppLowerWall;
                            }
                        }
                        else
                        {
                            if (i == door.position[1] && j == door.position[0] && key.isCollect)
                            {
                                roomArray[i, j] = door.sprite;
                            }
                            else
                            {
                                roomArray[i, j] = sideWall;
                            }
                        }
                    }  
                    else if (i == player.position[1] && j == player.position[0] || i == key.position[1] && j == key.position[0] )
                    { //checks if the loop passes on of the items/player cordinations 
                        if (i == player.position[1] && j == player.position[0])      //add the object to the room
                        {
                            roomArray[i, j] = player.sprite;
                        }
                        else
                        {
                            if (key.isCollect)
                            {
                                roomArray[i, j] = " ";
                            }
                            else
                            {
                                roomArray[i, j] = key.sprite;
                            }
                        }
                    }
                    else
                    {
                        roomArray[i, j] = " ";
                    }
                }
            }
            //saves into array
            return roomArray;
        }

        //Display generated array
        public void DrawBoard(string[,] roomArray, Player player, Key key, Door door, Menu mainMenu, bool withGameObject) {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (roomArray[i, j] == player.sprite && withGameObject)
                    {
                        mainMenu.PrinInColor(roomArray[i, j], ConsoleColor.Red, true);
                    } else if (roomArray[i, j] == key.sprite && withGameObject)
                    {
                        mainMenu.PrinInColor(roomArray[i, j], ConsoleColor.Green, true);
                    }
                    else if (roomArray[i, j] == door.sprite && withGameObject) {
                        mainMenu.PrinInColor(roomArray[i, j], ConsoleColor.Green, true);
                    }
                    else
                    {
                        Console.Write(roomArray[i,j]);
                    } 
                }
                Console.WriteLine();
            }
        }
    }
}
