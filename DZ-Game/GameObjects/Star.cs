using System;
using DZGame.GameInterfaces;
using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Star : IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        private float StarSpeed { get; set; }
        public Texture2D Image { get; set; }

        public Star(int screenWidth, int screenHeight, float starSpeed, Texture2D image)
        {
            //TODO - move private variables to fields.
            var r = new Random();
            Position_X = r.Next(screenWidth);
            Position_Y = r.Next(screenHeight);
            Position_Z = r.Next(1, 4);
            StarSpeed = starSpeed;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            Image = image;
        }

        public void MoveAuto(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.None);

            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_Y > ScreenHeight)
            {
                Position_Y = 0;
            }
        }

        public void MoveLeft(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.Left);

            Position_X += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X > ScreenWidth)
            {
                Position_X = 0;
            }
        }

        public void MoveRight(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.Right);

            Position_X -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X < 0)
            {
                Position_X = ScreenWidth;
            }
        }

        public void MoveUp(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.Up);

            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void MoveDown(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.Down);

            Position_Y -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private float GetStarSpeedMultiplier(int positionZ, Direction direction)
        {
            var starSpeedMultiplier = 0f;

            switch (direction)
            {
                case Direction.Left:
                case Direction.Right:
                case Direction.None:
                    switch (positionZ)
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
                    break;
                case Direction.Up:
                case Direction.Down:
                    switch (positionZ)
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
                    break;
            }

            return starSpeedMultiplier;
        }
    }
}
