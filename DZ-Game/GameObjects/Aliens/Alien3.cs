using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien3 : Alien
    {
        private int _centerX;
        private int _centerY;
        private double _angle;

        private const double AngularSpeed = 0.1;  // radians per frame (~6 deg/frame)
        private const double MinRadius = 30;
        private const double MaxRadius = 240;

        public Alien3(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image, int strength,
            int scoreValue, bool bulletsDestroyable, int shieldStrength, double initialAngle = 0)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _centerX = x;
            _centerY = y;
            _angle = initialAngle;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime)
        {
            _centerX += 3;
            if (_centerX > ScreenWidth - 35) _centerX = ScreenWidth - 35;
        }

        public override void MoveRight(GameTime gameTime)
        {
            _centerX -= 3;
            if (_centerX < 35) _centerX = 35;
        }

        public override void MoveUp(GameTime gameTime)
        {
            _centerY += 2;
            if (_centerY > ScreenHeight - 150) _centerY = ScreenHeight - 150;
        }

        public override void MoveDown(GameTime gameTime)
        {
            _centerY -= 2;
            if (_centerY < 40) _centerY = 40;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            _angle += AngularSpeed;

            // Radius oscillates between MinRadius and MaxRadius with a 2-second period
            double totalSeconds = gameTime.TotalGameTime.TotalSeconds;
            double radius = (MinRadius + MaxRadius) / 2.0
                          + ((MaxRadius - MinRadius) / 2.0) * Math.Sin(totalSeconds * Math.PI);

            PositionX = _centerX + (int)(radius * Math.Cos(_angle));
            PositionY = _centerY + (int)(radius * Math.Sin(_angle));

            PositionX = Math.Clamp(PositionX, 35, ScreenWidth - 35);
            PositionY = Math.Clamp(PositionY, 40, ScreenHeight - 150);

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
