using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien4: Traces a figure-8 (lemniscate) path that slowly drifts downward toward the player.
    // Each alien has its own phase offset, scale, and drift speed to make the swarm feel chaotic.
    public class Alien4 : Alien
    {
        private double _baseX;
        private double _baseY;
        private double _phase;         // individual time offset so each alien is out of sync
        private double _scaleX;        // width of the figure-8
        private double _scaleY;        // height of the figure-8
        private double _driftSpeed;    // how fast the alien drifts down the screen
        private double _timeAccum;     // accumulated game time

        public Alien4(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double phase, double scaleX, double scaleY, double driftSpeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _baseX = x;
            _baseY = y;
            _phase = phase;
            _scaleX = scaleX;
            _scaleY = scaleY;
            _driftSpeed = driftSpeed;
            _timeAccum = 0;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime)
        {
            _baseX += 3;
            if (_baseX > ScreenWidth - 35) _baseX = ScreenWidth - 35;
        }

        public override void MoveRight(GameTime gameTime)
        {
            _baseX -= 3;
            if (_baseX < 35) _baseX = 35;
        }

        public override void MoveUp(GameTime gameTime)
        {
            _baseY += 2;
            if (_baseY > ScreenHeight - 150) _baseY = ScreenHeight - 150;
        }

        public override void MoveDown(GameTime gameTime)
        {
            _baseY -= 2;
            if (_baseY < 40) _baseY = 40;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            _timeAccum += gameTime.ElapsedGameTime.TotalSeconds;

            // Lemniscate of Bernoulli parametric equations, scaled independently on each axis:
            //   x(t) = scaleX * cos(t) / (1 + sin²(t))
            //   y(t) = scaleY * sin(t)cos(t) / (1 + sin²(t))
            // This produces a smooth figure-8 lying on its side.
            double t = _timeAccum * 1.4 + _phase;
            double denom = 1.0 + Math.Sin(t) * Math.Sin(t);
            double lx = _scaleX * Math.Cos(t) / denom;
            double ly = _scaleY * Math.Sin(t) * Math.Cos(t) / denom;

            // Drift the baseline downward so aliens slowly menace the player
            _baseY += _driftSpeed * gameTime.ElapsedGameTime.TotalSeconds;

            // Bounce back up when reaching the lower danger zone, keeping them in the upper portion
            if (_baseY > ScreenHeight * 0.55)
                _baseY = 60;

            PositionX = (int)Math.Clamp(_baseX + lx, 35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(_baseY + ly, 40, ScreenHeight - 150);

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
