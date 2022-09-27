﻿using System;
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(titleSign.title);

            player.codeName = mainMenu.StartGameMessage();

            Console.ResetColor();
            Console.Clear();

            Console.WriteLine("Agent " + player.codeName + ", welcome aboard! We look forward to working with you. \n\nLet's go back to business. According to our sources, SAE is planning a secret weapon that will pose a danger to our \nPresident Joe Mama. One of our spies was dispatched to gather intel from the SAE's headquarters, but we lost contact \nwith him after a time. \n\nNow it's your turn to track him down and figure out what's going on at their headquarters, but first we need an \nestimate of the size of one of their buildings.");

            room.ConfirmSize();

            player.position = GeneratePosition(player.position, room.height - 1, room.width - 1);
            key.position = GeneratePosition(key.position, room.height - 1, room.width - 1);
            door.position = GeneratePosition(door.position, room.height - 1, room.width - 1);

           /* bool isOrganized = false;
            while (!isOrganized)
            {
                if (player.position[0] == key.position[0] && player.position[1] == key.position[1] || player.position[0] == door.position[0] && player.position[1] == door.position[1] || key.position[0] == door.position[0] && key.position[1] == door.position[1])
                {
                    if (player.position[0] == key.position[0] && player.position[1] == key.position[1])
                    {
                        key.position = GeneratePosition(key.position, room.height - 1, room.width - 1);
                    }
                    if (player.position[0] == door.position[0] && player.position[1] == door.position[1])
                    {
                        door.position = GeneratePosition(door.position, room.height - 1, room.width - 1);
                    }
                    if (key.position[0] == door.position[0] && key.position[1] == door.position[1])
                    {
                        door.position = GeneratePosition(door.position, room.height - 1, room.width - 1);
                    }
                }
                else
                {
                    isOrganized = true;
                }
            }*/

            room.GenerateRoom(room.width, room.height, player.position[0], player.position[1], player.sprite, key.position[0], key.position[1], key.sprite, door.position[0], door.position[1], door.sprite);

            Console.WriteLine("Player:");
            for (int i = 0; i < player.position.Length; i++)
            {
                Console.WriteLine(player.position[i]);
            }

            Console.WriteLine("Key:");
            for (int i = 0; i < key.position.Length; i++)
            {
                Console.WriteLine(key.position[i]);
            }

            Console.WriteLine("Door:");
            for (int i = 0; i < door.position.Length; i++)
            {
                Console.WriteLine(door.position[i]);
            }

            Console.WriteLine("Room:");
            Console.WriteLine("width: " +room.width);
            Console.WriteLine("height: " +room.height);
        }

        static int[] GeneratePosition(int[] objPosition, int roomHeight, int roomWidth)
        {
            Random randomNumber = new Random();
            objPosition = new int[] { randomNumber.Next(2, roomHeight), randomNumber.Next(2, roomWidth) };

            return objPosition; 
        }

        
    }
}
