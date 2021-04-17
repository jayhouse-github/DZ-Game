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
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Star(int screenWidth, int screenHeight, float starSpeed)
        {
            var r = new Random();
            Position_X = r.Next(screenWidth);
            Position_Y = r.Next(screenHeight);
            Position_Z = r.Next(1, 4);
            StarSpeed = starSpeed;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void MoveAuto()
        {
            var starSpeedMultiplier = 0f;

            switch (Position_Z)
            {
                case 1:
                    starSpeedMultiplier = StarSpeed;
                    break;
                case 2:
                    starSpeedMultiplier = StarSpeed * 2;
                    break;
                case 3:
                    starSpeedMultiplier = StarSpeed * 3;
                    break;
            }

            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_Y > ScreenHeight)
            {
                Position_Y = 0;
            }
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
