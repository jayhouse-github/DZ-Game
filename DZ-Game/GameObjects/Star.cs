using System;
using DZGame.Interfaces;
using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Star : IMovingObject
    {
        private readonly float _starSpeed;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public Texture2D Image { get; set; }
        public bool Active { get; set; }

        public Star(int screenWidth, int screenHeight, float starSpeed, Texture2D image)
        {
            var r = new Random();
            Position_X = r.Next(screenWidth);
            Position_Y = r.Next(screenHeight);
            Position_Z = r.Next(1, 4);
            _starSpeed = starSpeed;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Image = image;
            Active = true;
        }

        public void MoveAuto(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.None);

            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_Y > _screenHeight)
            {
                Position_Y = 0;
            }
        }

        public void MoveLeft(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(Position_Z, Direction.Left);

            Position_X += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X > _screenWidth)
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
                Position_X = _screenWidth;
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
                    switch (positionZ)
                    {
                        case 1:
                            starSpeedMultiplier = _starSpeed / 2;
                            break;
                        case 2:
                            starSpeedMultiplier = _starSpeed;
                            break;
                        case 3:
                            starSpeedMultiplier = _starSpeed * 2;
                            break;
                    }
                    break;
                case Direction.None:
                    switch (positionZ)
                    {
                        case 1:
                            starSpeedMultiplier = _starSpeed;
                            break;
                        case 2:
                            starSpeedMultiplier = _starSpeed * 2;
                            break;
                        case 3:
                            starSpeedMultiplier = _starSpeed * 3;
                            break;
                    }
                    break;
                case Direction.Up:
                case Direction.Down:
                    switch (positionZ)
                    {
                        case 1:
                            starSpeedMultiplier = _starSpeed / 2;
                            break;
                        case 2:
                            starSpeedMultiplier = _starSpeed;
                            break;
                        case 3:
                            starSpeedMultiplier = _starSpeed * 2;
                            break;
                    }
                    break;
            }

            return starSpeedMultiplier;
        }
    }
}
