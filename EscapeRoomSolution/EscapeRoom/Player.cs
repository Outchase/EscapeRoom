using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Player
    {
        ConsoleKeyInfo keyinfo = new ConsoleKeyInfo();  

        public string codeName;

        public bool didEscape = false;

        //player movement
        public void Movement(Room room)
        {
            bool rightKeyPressed = false;

            //listens to key input and changes player cordinates
            while (!rightKeyPressed)
            {
                keyinfo = Console.ReadKey(true);

                if (position[0] == 2 && position[1] == 2)
                {
                    //player is top left
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                }
                else if (position[0] == room.width - 1 && position[1] == 2)
                { 
                    //player is top right
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                    }
                }
                else if (position[0] == 2 && position[1] == room.height - 1)
                { 
                    //player is bottom left
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                }
                else if (position[0] == room.width - 1 && position[1] == room.height - 1) 
                {
                    //player is bottom right
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                    }
                }
                else if (position[0] > 2 && position[0] < room.width - 1 && position[1] == 2) {
                    //player is top edge
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                } else if (position[0] > 2 && position[0] < room.width - 1 && position[1] == room.height - 1) {
                    //player is bottom edge
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                } else if (position[1] > 2 && position[1] < room.height - 1 && position[0] == 2) {
                    //player is left edge
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                } else if (position[1] > 2 && position[1] < room.height - 1 && position[0] == room.width-1) {
                    //player is right edge
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                    }
                }
                else
                {
                    switch (keyinfo.Key)
                    {
                        case ConsoleKey.W:
                            position[1]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.S:
                            position[1]++;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.A:
                            position[0]--;
                            rightKeyPressed = true;
                            break;
                        case ConsoleKey.D:
                            position[0]++;
                            rightKeyPressed = true;
                            break;
                    }
                }
            }

            //set the position of cursor to 0 & avoid flickering
            Console.SetCursorPosition(Console.CursorLeft=0,Console.CursorTop=0);
        }

        public int[] position = {0,0};

        public string sprite = "■";

    }
}
