using System;
using System.Numerics;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using EscapeRoom.Properties;

namespace EscapeRoom
{
    internal class Program
    {
        static void Main()
        {
            ASCIISign titleSign = new();
            Menu mainMenu = new();
            Player player = new();
            Key key = new();
            Door door = new();
            Room room = new();
            bool wantToPlayAgain = true;

            while (wantToPlayAgain)
            {
                wantToPlayAgain= StartGame(titleSign, mainMenu, player, key, door, room, wantToPlayAgain);
            }

            mainMenu.PrinInColor(titleSign.outro, ConsoleColor.Yellow, true);
            Environment.Exit(0);
        }

        private static void Exit()
        {
            throw new NotImplementedException();
        }

        //Intro screen will be displayed 
        static bool StartGame(ASCIISign titleSign, Menu mainMenu, Player player, Key key, Door door, Room room, bool wantToPlayAgain)
        {
            //set variables to default
            key.isCollect = false;
            player.didEscape = false;
            bool isGameOver = false;
            player.position = new int[] { 0, 0 };
            key.position = new int[] { 0, 0 };
            door.position = new int[] { 0, 0 };


            //display game title inguding game start interaction
            mainMenu.PrinInColor(titleSign.title, ConsoleColor.Yellow, false);
            player.codeName = mainMenu.StartGameMessage();
            Console.ResetColor();
            Console.Clear();

            //Game introduction coloring important information
            Console.Write(Resources.agentNoun);
            mainMenu.PrinInColor(player.codeName, ConsoleColor.Green, true);
            Console.Write(Resources.introEstimateRoom);

            
            room.ConfirmSize(player, key, door, mainMenu);

            //generate x, y cordination of game objects
            player.position = GeneratePosition(player.position, room, false);
            key.position = GeneratePosition(key.position, room, false);
            door.position = GeneratePosition(door.position, room, true);


            //verify position of all game objects and generate new until alright
            PositionVerify(player.position, key.position, door.position, room);

            //displaying the actual game
            wantToPlayAgain = Gameplay(isGameOver, player, room, key, door, mainMenu, titleSign, wantToPlayAgain);

            return wantToPlayAgain;
        }

        //generates a random x,y corrdinate depending on the roomn size
        static int[] GeneratePosition(int[] objPosition, Room room, bool isDoor)
        {
            Random randomNumber = new();

            int[] edgeHeightPosition = new int[] { 0, room.height-1 }; //rework

            int xPosition = randomNumber.Next(1, room.width-2);
            int yPosition = randomNumber.Next(1, room.height-2);

            if (!isDoor)
            {
                objPosition = new int[] { xPosition, yPosition };
            }
            else
            {
                xPosition = randomNumber.Next(0, room.width);

                if (xPosition > 0 && xPosition < room.width-1)
                {
                    objPosition = new int[] { xPosition, edgeHeightPosition[randomNumber.Next(2)]};
                }
                else {
                    objPosition = new int[] { xPosition, randomNumber.Next(1, room.height-1)};
                }
            }

            return objPosition;
        }

        //Verifies and avoid game objects spawn at the same spot 
        static void PositionVerify(int[] playerPosition, int[] keyPosition, int[] doorPosition, Room room)
        {
            bool isOrganized = false;

            while (!isOrganized)
            {
                if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1] || playerPosition[0] == doorPosition[0] && playerPosition[1] == doorPosition[1] || keyPosition[0] == doorPosition[0] && keyPosition[1] == doorPosition[1])
                {
                    if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1])
                    {
                        keyPosition = GeneratePosition(keyPosition, room, false);
                    }
                }
                else
                {
                    isOrganized = true;
                }
            }
        }

        //starts the actual gameplay
        static bool Gameplay(bool isGameOver, Player player, Room room, Key key, Door door, Menu mainMenu, ASCIISign titleSign, bool wantToPlayAgain)
        {
            //listens to interactions until game is over
            while (!isGameOver)
            {
                //Display Controls
                Console.Write(Resources.introControls);
                mainMenu.PrinInColor(Resources.WASD, ConsoleColor.Green, true);
                Console.Write(Resources.controlsKeys);
                mainMenu.PrinInColor(Char.ToString(key.sprite), ConsoleColor.Green, true);
                Console.Write(Resources.controlsAfterDoor);
                mainMenu.PrinInColor(Char.ToString(door.sprite), ConsoleColor.Green, true);
                Console.Write(Resources.controlsDoor);
                mainMenu.PrinInColor(Resources.mission, ConsoleColor.Yellow, true);

                //set true when player collects the key
                if (player.position[0] == key.position[0] && player.position[1] == key.position[1] && !key.isCollect)
                {
                    key.isCollect = true;
                }
                //change color of objective when succeed
                if (key.isCollect)
                {
                    mainMenu.PrinInColor(Resources.objectiveKeycard, ConsoleColor.Green, true);
                }
                else
                {
                    Console.Write(Resources.objectiveKeycard);
                }

                //set true when player collected the key and enters the door
                if (player.position[0] == door.position[0] && player.position[1] == door.position[1] && key.isCollect)
                {
                    mainMenu.PrinInColor(Resources.objectiveEscape, ConsoleColor.Green, true);
                    isGameOver = true;
                    Console.Clear();
                }
                else
                {
                    Console.Write(Resources.objectiveEscape);
                    room.roomArray = room.GenerateRoom(player, key, door);
                    room.DrawBoard(room.roomArray, player, key, door, mainMenu, true);
                    player.Movement(room, door, key);
                    Console.Beep();
                }
            }

            //Display complete Mission sign and prompt to retry
            mainMenu.PrinInColor(titleSign.missionComplete, ConsoleColor.Yellow, true);
            //recall the method when user wants to playa again
            wantToPlayAgain = RestartGame(mainMenu, wantToPlayAgain);
            
            Console.Clear();
            return wantToPlayAgain;

           
        }

        //prompt weather the user wants to play another round return true or false
        static bool RestartGame(Menu mainMenu, bool wantToPlayAgain)
        {

            ConsoleKeyInfo userKeyInput;
            bool keyPressedRight = false;
            

            mainMenu.PrinInColor(Resources.tryAgainPrompt, ConsoleColor.Green, true);

            while (!keyPressedRight)
            {

                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Y)
                {
                    keyPressedRight = true;
                }
                else if (userKeyInput.Key == ConsoleKey.N)
                {
                    keyPressedRight = true;
                    wantToPlayAgain = false;
                }
            }

            return wantToPlayAgain;
        }
    }
}
