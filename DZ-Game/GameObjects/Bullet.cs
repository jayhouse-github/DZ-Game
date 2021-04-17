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

        public void MoveAuto()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }
    }
}
