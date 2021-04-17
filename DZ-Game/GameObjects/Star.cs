using System;
using DZGame.GameInterfaces;

namespace DZGame.GameObjects
{
    public class Star : IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public float StarSpeed { get; set; }

        public Star(int screenWidth, int screenHeight, float starSpeed)
        {
            var r = new Random();
            Position_X = r.Next(screenWidth);
            Position_Y = r.Next(screenHeight);
            Position_Z = r.Next(1, 4);
            StarSpeed = starSpeed;
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
