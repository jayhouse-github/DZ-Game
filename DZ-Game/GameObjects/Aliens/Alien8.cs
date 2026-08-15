using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien8: Moves diagonally, bouncing off screen edges like a billiard ball.
    // Constrained to the upper region of the screen. Each alien has a distinct
    // velocity angle, making the swarm unpredictable. High shield strength.
    public class Alien8 : Alien
    {
        private double _posX;
        private double _posY;
        private double _velX;  // px/s
        private double _velY;  // px/s

        public Alien8(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double velX, double velY)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _posX = x;
            _posY = y;
            _velX = velX;
            _velY = velY;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime) { _posX += 3; }
        public override void MoveRight(GameTime gameTime) { _posX -= 3; }
        public override void MoveUp(GameTime gameTime) { _posY += 2; }
        public override void MoveDown(GameTime gameTime) { _posY -= 2; }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;

            _posX += _velX * dt;
            _posY += _velY * dt;

            int maxY = (int)(ScreenHeight * 0.44);

            if (_posX <= 35) { _posX = 35; _velX = Math.Abs(_velX); }
            if (_posX >= ScreenWidth - 35) { _posX = ScreenWidth - 35; _velX = -Math.Abs(_velX); }
            if (_posY <= 40) { _posY = 40; _velY = Math.Abs(_velY); }
            if (_posY >= maxY) { _posY = maxY; _velY = -Math.Abs(_velY); }

            PositionX = (int)_posX;
            PositionY = (int)_posY;

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
