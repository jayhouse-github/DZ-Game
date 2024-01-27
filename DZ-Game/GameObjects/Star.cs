using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Star : BaseMovingObject
    {
        private readonly float _starSpeed;
        
        public Star(int x, int y, int z, float starSpeed, Texture2D image, int screenWidth, int screenHeight) 
            : base(x, y, z, image, screenWidth, screenHeight)
        {
            _starSpeed = starSpeed;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(PositionZ, Direction.None);

            PositionY += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (PositionY > ScreenHeight)
            {
                PositionY = 0;
            }
        }

        public override void MoveLeft(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(PositionZ, Direction.Left);

            PositionX += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (PositionX > ScreenWidth)
            {
                PositionX = 0;
            }
        }

        public override void MoveRight(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(PositionZ, Direction.Right);

            PositionX -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (PositionX < 0)
            {
                PositionX = ScreenWidth;
            }
        }

        public override void MoveUp(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(PositionZ, Direction.Up);

            PositionY += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public override void MoveDown(GameTime gameTime)
        {
            var starSpeedMultiplier = GetStarSpeedMultiplier(PositionZ, Direction.Down);

            PositionY -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
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
