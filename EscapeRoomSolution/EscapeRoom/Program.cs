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

        static void StartGame(ASCIISign titleSign, Menu mainMenu, Player player, Key key, Door door, Room room) {

            key.isCollect = false;
            player.didEscape = false;

            mainMenu.PrinInColor(titleSign.title, ConsoleColor.Yellow, false);

            player.codeName = mainMenu.StartGameMessage();

            Console.ResetColor();
            Console.Clear();

            Console.Write("Agent ");
            mainMenu.PrinInColor(player.codeName, ConsoleColor.Green, true);
            Console.Write(", welcome aboard! We look forward to working with you. \n\nLet's go back to business. According to our sources, SAE is planning a secret weapon that will pose a danger to our \nPresident Joe Mama. One of our spies was dispatched to gather intel from the SAE's headquarters, but we lost contact \nwith him after a time. \n\nNow it's your turn to track him down and figure out what's going on at their headquarters, but first we need an \nestimate of the size of one of their buildings.\n");

            room.ConfirmSize();


            player.position = GeneratePosition(player.position, room.height, room.width, false);
            key.position = GeneratePosition(key.position, room.height, room.width, false); //not the the room height
            door.position = GeneratePosition(door.position, room.height, room.width, true);

            PositionVerify(player.position, key.position, door.position, room.width, room.height);

            Game(false, player, room, key, door, mainMenu, titleSign);
        }


        static int[] GeneratePosition(int[] objPosition, int roomHeight, int roomWidth, bool isDoor)
        {
            Random randomNumber = new Random();

            int[] edgeHeightPosition= new int[] {2,roomHeight-1};
            int xPosition = randomNumber.Next(2, roomWidth);
            int yPosition = randomNumber.Next(2, roomHeight);

            if (!isDoor)
            {
                objPosition = new int[] { xPosition, yPosition};
            }
            else
            {
                if (xPosition != 2 || xPosition != roomWidth) { 
                objPosition = new int[] {xPosition, edgeHeightPosition[randomNumber.Next(2)]};
                }
            }
                return objPosition;
        }

        static void PositionVerify(int[] playerPosition, int[] keyPosition, int[] doorPosition, int roomWidth, int roomHeight)
        {
            bool isOrganized = false;

            while (!isOrganized)
            {
                if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1] || playerPosition[0] == doorPosition[0] && playerPosition[1] == doorPosition[1] || keyPosition[0] == doorPosition[0] && keyPosition[1] == doorPosition[1])
                {
                    Console.WriteLine("same");
                    if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1])
                    {
                        keyPosition = GeneratePosition(keyPosition, roomHeight, roomWidth, false);
                    }
                    if (playerPosition[0] == doorPosition[0] && playerPosition[1] == doorPosition[1])
                    {
                        doorPosition = GeneratePosition(doorPosition, roomHeight, roomWidth, true);
                    }
                    if (keyPosition[0] == doorPosition[0] && keyPosition[1] == doorPosition[1])
                    {
                        doorPosition = GeneratePosition(doorPosition, roomHeight, roomWidth, true);
                    }
                }
                else
                {
                    isOrganized = true;
                }
            }
        }

        static void Game(bool gameOver, Player player, Room room, Key key, Door door, Menu mainMenu, ASCIISign titleSign)
        {
            while (!gameOver)
            {
                Console.Write("Before we begin the mission, I'd want to provide you with a few advice. \nYou can move around by using the ");
                mainMenu.PrinInColor("W,A,S,D", ConsoleColor.Green, true);
                Console.Write(" keys:\n ↑ To move up, use the W key\n ↓ To go down, use the S key\n ← To go left, use the A key\n → To go right, use the D key \nYou may collect it by wallking over the key card (");
                mainMenu.PrinInColor(key.sprite, ConsoleColor.Green, true);
                Console.Write(")\nAfter then, the door (");
                mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true);
                Console.Write(") will be visible and may be entered by heading to it.\n");
                mainMenu.PrinInColor("\nMission:\n", ConsoleColor.Yellow, true);

                if (player.position[0] == key.position[0] && player.position[1] == key.position[1] && !key.isCollect)
                {
                    key.isCollect = true;
                }

                if (player.position[0] == door.position[0] && player.position[1] == door.position[1] && key.isCollect)
                {
                    player.didEscape = true;
                }

                if (key.isCollect)
                {
                    mainMenu.PrinInColor("■ Collect the Keycard\n", ConsoleColor.Green, true);
                }
                else { 
                Console.WriteLine("■ Collect the Keycard");
                }

                if (player.didEscape)
                {
                    mainMenu.PrinInColor("■ Escape from the HQ\n", ConsoleColor.Green, true);
                    gameOver = true;
                }
                else
                {
                    Console.WriteLine("■ Escape from the HQ");
                    room.GenerateRoom(player, key, door);
                    player.Movement(room);
                }
            }
            
            mainMenu.PrinInColor(titleSign.missionComplete, ConsoleColor.Yellow, true);
            bool wantToPlayAgain = RestartGame(mainMenu, titleSign);
            Console.Clear();

            if (wantToPlayAgain)
            {
                StartGame(titleSign, mainMenu, player, key, door, room);
            }
            else {
                mainMenu.PrinInColor(titleSign.outro, ConsoleColor.Yellow, true);
            }
        }

        static bool RestartGame(Menu mainMenu, ASCIISign titleSign) {

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
                    wantToPlayAgain= true;
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
