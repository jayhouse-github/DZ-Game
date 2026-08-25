using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien8: Fast, erratic bouncer. Moves at high speed and randomly changes direction
    // at short intervals, making it highly unpredictable. No dive behaviour.
    public class Alien8 : Alien
    {
        private double _posX;
        private double _posY;
        private double _velX;  // px/s
        private double _velY;  // px/s
        private readonly double _baseSpeed;
        private double _dirChangeTimer;
        private double _nextChangeInterval;
        private readonly Random _rng;

        public Alien8(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double velX, double velY, int seed = 0)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _posX = x;
            _posY = y;
            _velX = velX;
            _velY = velY;
            _baseSpeed = Math.Sqrt(velX * velX + velY * velY);
            _rng = new Random(seed);
            _dirChangeTimer = 0;
            _nextChangeInterval = 0.3 + _rng.NextDouble() * 0.5;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime) { _posX += 3; }
        public override void MoveRight(GameTime gameTime) { _posX -= 3; }
        public override void MoveUp(GameTime gameTime) { _posY += 2; }
        public override void MoveDown(GameTime gameTime) { _posY -= 2; }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;

            _dirChangeTimer += dt;
            if (_dirChangeTimer >= _nextChangeInterval)
            {
                _dirChangeTimer = 0;
                _nextChangeInterval = 0.3 + _rng.NextDouble() * 0.5;
                double newAngle = _rng.NextDouble() * Math.PI * 2;
                double speedVar = _baseSpeed * (0.85 + _rng.NextDouble() * 0.30);
                _velX = speedVar * Math.Cos(newAngle);
                _velY = speedVar * Math.Sin(newAngle);
            }

            _posX += _velX * dt;
            _posY += _velY * dt;

            int maxY = (int)(ScreenHeight * 0.65);

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
