using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien7: Sweeps horizontally across the screen while its Y position oscillates in a
    // sine wave, creating a weaving movement. High shield strength — takes multiple hits.
    public class Alien7 : Alien
    {
        private double _posX;
        private double _velX;        // px/s horizontal sweep speed
        private readonly double _baseY;
        private readonly double _amplitude;  // px — vertical oscillation range
        private readonly double _frequency;  // rad/s — oscillation speed
        private readonly double _phase;      // rad — per-alien phase offset
        private double _timeAccum;

        public Alien7(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double velX, double amplitude, double frequency, double phase)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _posX = x;
            _velX = velX;
            _baseY = y;
            _amplitude = amplitude;
            _frequency = frequency;
            _phase = phase;
            _timeAccum = 0;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime)
        {
            _posX += 3;
            if (_posX > ScreenWidth - 35) _posX = ScreenWidth - 35;
        }

        public override void MoveRight(GameTime gameTime)
        {
            _posX -= 3;
            if (_posX < 35) _posX = 35;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _timeAccum += dt;

            _posX += _velX * dt;

            if (_posX <= 35) { _posX = 35; _velX = Math.Abs(_velX); }
            if (_posX >= ScreenWidth - 35) { _posX = ScreenWidth - 35; _velX = -Math.Abs(_velX); }

            double posY = _baseY + _amplitude * Math.Sin(_timeAccum * _frequency + _phase);

            PositionX = (int)Math.Clamp(_posX, 35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(posY, 40, (int)(ScreenHeight * 0.42));

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
