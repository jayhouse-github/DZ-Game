using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;

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

        public void MoveAuto(GameTime gameTime)
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

        public void MoveLeft(GameTime gameTime)
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

            Position_X += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X > ScreenWidth)
            {
                Position_X = 0;
            }
        }

        public void MoveRight(GameTime gameTime)
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

            Position_X -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X < 0)
            {
                Position_X = ScreenWidth;
            }
        }

        public void MoveUp(GameTime gameTime)
        {
            var starSpeedMultiplier = 0f;

            switch (Position_Z)
            {
                case 1:
                    starSpeedMultiplier = StarSpeed / 2;
                    break;
                case 2:
                    starSpeedMultiplier = StarSpeed;
                    break;
                case 3:
                    starSpeedMultiplier = StarSpeed * 2;
                    break;
            }

            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void MoveDown(GameTime gameTime)
        {
            var starSpeedMultiplier = 0f;

            switch (Position_Z)
            {
                case 1:
                    starSpeedMultiplier = StarSpeed / 2;
                    break;
                case 2:
                    starSpeedMultiplier = StarSpeed;
                    break;
                case 3:
                    starSpeedMultiplier = StarSpeed * 2;
                    break;
            }

            Position_Y -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
