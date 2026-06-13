using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien2 : Alien
    {
        private bool _isHorizontalMove;
        private int currentDirection = 1;

        public Alien2(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image, int strength,
            int scoreValue, bool bulletsDestroyable, int shieldStrength) 
            : base (x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            Active = true;
            _isHorizontalMove = new Random().Next(2) == 0;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            if (_isHorizontalMove)
            {
                PositionX += (currentDirection == 1 ? 15 : -15);
                if (PositionX >= ScreenWidth - Image.Width || PositionX <= 0)
                {
                    currentDirection *= -1;
                }
            }
            else
            {
                PositionY += (currentDirection == 1 ? 10 : -10);
                if (PositionY >= ScreenHeight - Image.Height || PositionY <= 0)
                {
                    currentDirection *= -1;
                }
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
