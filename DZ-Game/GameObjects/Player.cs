using System;
namespace DZGame.GameObjects
{
    public class Player
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }

        public Player(int x, int y, int z)
        {
            Position_X = x;
            Position_Y = y;
            Position_Z = z;
        }
    }
}
