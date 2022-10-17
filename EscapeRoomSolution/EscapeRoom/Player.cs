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

        public bool didEscape;

        //player movement

        public void Movement(Room room, Door door, Key key)
        {
            bool rightKeyPressed = false;

            //listens to key input and changes player cordinates
            while (!rightKeyPressed)
            {
                keyinfo = Console.ReadKey(true);

                switch (keyinfo.Key)
                {
                    case ConsoleKey.W:
                        if (position[1]!=1 || key.isCollect && position[1] == door.position[1]+1 && position[0] == door.position[0])
                        { 
                        position[1]--;
                        }
                        rightKeyPressed = true;
                        break;
                    case ConsoleKey.S:
                        if (position[1] != room.height - 2 || key.isCollect && position[1] == door.position[1] - 1 && position[0] == door.position[0])
                        {
                            position[1]++;
                        }
                        rightKeyPressed = true;
                        break;
                    case ConsoleKey.A:
                        if (position[0] != 1 || key.isCollect && position[0] == door.position[0] + 1 && position[1] == door.position[1])
                        {
                            position[0]--;
                        }
                        rightKeyPressed = true;
                        break;
                    case ConsoleKey.D:
                        if (position[0] != room.width - 2 || key.isCollect && position[0] == door.position[0] - 1 && position[1] == door.position[1])
                        {
                            position[0]++;
                        }
                        rightKeyPressed = true;
                        break;
                }
            }
            //set the position of cursor to 0 & avoid flickering
            Console.SetCursorPosition(Console.CursorLeft = 0, Console.CursorTop = 0);
        }

    public int[] position;

    public char sprite = '\u25A0';

    }
}
