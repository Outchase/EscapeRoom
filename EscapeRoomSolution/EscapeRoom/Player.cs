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

        public void Movement(Room room)
        {

            bool rightKeyPressed = false;
            
            while (!rightKeyPressed) {
                keyinfo = Console.ReadKey(true);

               /* if (position[0] == 2 || position[0] == room.width - 1 || position[1] == 2 || position[1] == room.height - 1)
                {
                    if (position[0] == 2 && position[1] == 2) {
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
                    } else if (position[0] == 2 && position[1] == 2) {
                }
                else {
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
                }*/
            }
        }

        public int[] position = {};

        public string sprite = "■";

    }
}
