using System;
using DZGame.GameInterfaces;

namespace DZGame.GameObjects
{
    public class Bullet : IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }

        public Bullet(int x, int y)
        {
            Position_X = x;
            Position_Y = y;
        }
    }
}
