using System;
using System.Numerics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EscapeRoom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASCIISign titleSign = new ASCIISign();
            Menu mainMenu = new Menu();
            Player player = new Player();
            Key key = new Key();
            Door door = new Door();
            Room room = new Room();

            StartGame(titleSign, mainMenu, player, key, door, room);
        }

        //Intro screen will be displayed 
        static void StartGame(ASCIISign titleSign, Menu mainMenu, Player player, Key key, Door door, Room room)
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
            Console.Write("Agent ");
            mainMenu.PrinInColor(player.codeName, ConsoleColor.Green, true);
            Console.Write(", welcome aboard! We look forward to working with you. \n\nLet's go back to business. According to our sources, SAE is planning a secret weapon that will pose a danger to our \nPresident Joe Mama. One of our spies was dispatched to gather intel from the SAE's headquarters, but we lost contact \nwith him after a time. \n\nNow it's your turn to track him down and figure out what's going on at their headquarters, but first we need an \nestimate of the size of one of their buildings.\n");

            
            room.ConfirmSize(player, key, door, mainMenu);

            //generate x, y cordination of game objects
            player.position = GeneratePosition(player.position, room, false);
            key.position = GeneratePosition(key.position, room, false);
            door.position = GeneratePosition(door.position, room, true);

            //Console.WriteLine("Door:\n x: " + door.position[0] + "\n Y:" + door.position[1]);

            //verify position of all game objects and generate new until alright
            PositionVerify(player.position, key.position, door.position, room);

            //displaying the actual game
            Gameplay(isGameOver, player, room, key, door, mainMenu, titleSign);
        }

        //generates a random x,y corrdinate depending on the roomn size
        static int[] GeneratePosition(int[] objPosition, Room room, bool isDoor)
        {
            Random randomNumber = new Random();

            int[] edgeHeightPosition = new int[] { 0, room.height-1 }; //rework

           // Console.WriteLine(room.width);

            /*for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(randomNumber.Next(1, room.height-1));
            }*/

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
                    Console.WriteLine("same");
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
        static void Gameplay(bool isGameOver, Player player, Room room, Key key, Door door, Menu mainMenu, ASCIISign titleSign)
        {
            //listens to interactions until game is over
            while (!isGameOver)
            {
                //Display Controls
                Console.Write("Before we begin the mission, I'd want to provide you with a few advice. \nYou can move around by using the ");
                mainMenu.PrinInColor("W,A,S,D", ConsoleColor.Green, true);
                Console.Write(" keys:\n ↑ To move up, use the W key\n ↓ To go down, use the S key\n ← To go left, use the A key\n → To go right, use the D key \nYou may collect it by wallking over the key card (");
                mainMenu.PrinInColor(key.sprite, ConsoleColor.Green, true);
                Console.Write(")\nAfter then, the door (");
                mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true);
                Console.Write(") will be visible and may be entered by heading to it.\n");
                mainMenu.PrinInColor("\nMission:\n", ConsoleColor.Yellow, true);

                //set true when player collects the key
                if (player.position[0] == key.position[0] && player.position[1] == key.position[1] && !key.isCollect)
                {
                    key.isCollect = true;
                }
                //change color of objective when succeed
                if (key.isCollect)
                {
                    mainMenu.PrinInColor("■ Collect the Keycard\n", ConsoleColor.Green, true);
                }
                else
                {
                    Console.WriteLine("■ Collect the Keycard");
                }

                //set true when player collected the key and enters the door
                if (player.position[0] == door.position[0] && player.position[1] == door.position[1] && key.isCollect)
                {
                    mainMenu.PrinInColor("■ Escape from the HQ\n", ConsoleColor.Green, true);
                    isGameOver = true;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("■ Escape from the HQ");
                    room.roomArray = room.GenerateRoom(player, key, door);
                    room.DrawBoard(room.roomArray, player, key, door, mainMenu, true);
                    player.Movement(room, door, key);
                    Console.Beep();
                }
            }

            //Display complete Mission sign and prompt to retry
            mainMenu.PrinInColor(titleSign.missionComplete, ConsoleColor.Yellow, true);
            bool wantToPlayAgain = RestartGame(mainMenu);
            Console.Clear();

            if (wantToPlayAgain) //recall the method when user wants to playa again
            {
                StartGame(titleSign, mainMenu, player, key, door, room);
            }
            else  //displays outro message 
            {
                mainMenu.PrinInColor(titleSign.outro, ConsoleColor.Yellow, true);
            }
        }

        //prompt weather the user wants to play another round return true or false
        static bool RestartGame(Menu mainMenu)
        {

            ConsoleKeyInfo userKeyInput;
            bool keyPressedRight = false;
            bool wantToPlayAgain = false;

            mainMenu.PrinInColor("Do you want to try again? [Y/n] ", ConsoleColor.Green, true);

            while (!keyPressedRight)
            {

                userKeyInput = Console.ReadKey(true);

                if (userKeyInput.Key == ConsoleKey.Y)
                {
                    keyPressedRight = true;
                    wantToPlayAgain = true;
                }
                else if (userKeyInput.Key == ConsoleKey.N)
                {
                    keyPressedRight = true;
                }
            }

            return wantToPlayAgain;
        }
    }
}
