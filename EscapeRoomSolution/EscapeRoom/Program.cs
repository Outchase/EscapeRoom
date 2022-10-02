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
            Random randomNumber = new Random();
            ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();

            mainMenu.PrinInColor(titleSign.title, ConsoleColor.Yellow, false);

            player.codeName = mainMenu.StartGameMessage();

            Console.ResetColor();
            Console.Clear();

            Console.Write("Agent ");
            mainMenu.PrinInColor(player.codeName, ConsoleColor.Green, true);
            Console.Write(", welcome aboard! We look forward to working with you. \n\nLet's go back to business. According to our sources, SAE is planning a secret weapon that will pose a danger to our \nPresident Joe Mama. One of our spies was dispatched to gather intel from the SAE's headquarters, but we lost contact \nwith him after a time. \n\nNow it's your turn to track him down and figure out what's going on at their headquarters, but first we need an \nestimate of the size of one of their buildings.\n");

            room.ConfirmSize();

            player.position = GeneratePosition(player.position, room.height - 1, room.width - 1);
            key.position = GeneratePosition(key.position, room.height - 1, room.width - 1); //not the the room height
            door.position = GeneratePosition(door.position, room.height - 1, room.width - 1);


            PositionVerify(player.position, key.position, door.position, room.width, room.height);

            Console.Write("Before we begin the mission, I'd want to provide you with a few advice. \nYou can move around by using the ");
            mainMenu.PrinInColor("W,A,S,D", ConsoleColor.Green, true);
            Console.Write(" keys:\n ↑ To move up, use the W key\n ↓ To go down, use the S key\n ← To go left, use the A key\n → To go right, use the D key \nYou may collect it by wallking over the key card (");
            mainMenu.PrinInColor(key.sprite, ConsoleColor.Green, true);
            Console.Write(")\nAfter then, the door (");
            mainMenu.PrinInColor(door.sprite, ConsoleColor.Green, true); 
            Console.Write(") will be visible and may be entered by heading to it.");
            Console.WriteLine();

            room.GenerateRoom(room.width, room.height, player.position[0], player.position[1], player.sprite, key.position[0], key.position[1], key.sprite, door.position[0], door.position[1], door.sprite);

            Console.WriteLine("Player Array Position:\nX:"+ player.position[0] +"\nY:" + player.position[1]);
            Console.WriteLine("Key Array Position:\nX:"+ key.position[0] +"\nY:" + key.position[1]);
            Console.WriteLine("Door Array Position:\nX:"+ door.position[0] +"\nY:" + door.position[1]);
            
            /*for (int i = 0; i < player.position.Length; i++)
            {
                Console.WriteLine(player.position[i]);
            }
            
            Console.WriteLine("Key X Y Array:");
            for (int i = 0; i < key.position.Length; i++)
            {
                Console.WriteLine(key.position[i]);
            }

            Console.WriteLine("Door Array:");
            for (int i = 0; i < door.position.Length; i++)
            {
                Console.WriteLine(door.position[i]);
            }

            Console.WriteLine("Room:");
            Console.WriteLine("width: " +room.width);
            Console.WriteLine("height: " +room.height);*/

            keyinfo = Console.ReadKey(true);

            player.Movement(keyinfo);

        }

        static int[] GeneratePosition(int[] objPosition, int roomHeight, int roomWidth)
        {
            Random randomNumber = new Random();
            objPosition = new int[] { randomNumber.Next(2, roomWidth), randomNumber.Next(2, roomHeight) };

            return objPosition; 
        }
        static void PositionVerify(int[] playerPosition, int[] keyPosition, int[] doorPosition, int roomWidth, int roomHeight) {
            bool isOrganized = false;
            //Console.WriteLine("Player position:\nX:" + playerPosition[0]+"\nY:"+playerPosition);
            while (!isOrganized)
            {
                if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1] || playerPosition[0] == doorPosition[0] && playerPosition[1] == doorPosition[1] || keyPosition[0] == doorPosition[0] && keyPosition[1] == doorPosition[1])
                {
                    Console.WriteLine("same");
                    if (playerPosition[0] == keyPosition[0] && playerPosition[1] == keyPosition[1])
                    {
                        keyPosition = GeneratePosition(keyPosition, roomHeight - 1, roomWidth - 1);
                        Console.WriteLine("keyPos to player changed");
                    }
                    if (playerPosition[0] == doorPosition[0] && playerPosition[1] == doorPosition[1])
                    {
                        doorPosition = GeneratePosition(doorPosition, roomHeight - 1, roomWidth - 1);
                        Console.WriteLine("DoorPos to Player changed");
                    }
                    if (keyPosition[0] == doorPosition[0] && keyPosition[1] == doorPosition[1])
                    {
                        doorPosition = GeneratePosition(doorPosition, roomHeight - 1, roomWidth - 1);
                        Console.WriteLine("DoorPos to key changed");
                    }
                    Console.WriteLine("New Player position:\nX:" + playerPosition[0] + "\nY:" + playerPosition[1]);
                    Console.WriteLine("New Key position:\nX:" + keyPosition[0] + "\nY:" + keyPosition[1]);
                }
                else
                {
                    isOrganized = true;
                }
            }
        }
        
    }
}
