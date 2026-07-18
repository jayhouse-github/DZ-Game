using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien5: Traces a Lissajous curve (3:2 frequency ratio) for a complex, fast looping path.
    // Each alien has a different phase delta giving unique trefoil-like shapes. They also share
    // a slow drifting centre so the whole swarm gradually migrates around the screen.
    public class Alien5 : Alien
    {
        private double _centerX;
        private double _centerY;
        private double _amplX;       // amplitude on X axis
        private double _amplY;       // amplitude on Y axis
        private double _phaseDelta;  // phase shift between X and Y, shapes the curve
        private double _speed;       // angular speed in radians per second
        private double _driftAngle;  // direction the centre drifts
        private double _driftSpeed;  // pixels per second the centre moves
        private double _time;

        public Alien5(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double phaseDelta, double amplX, double amplY, double speed, double driftAngle, double driftSpeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _centerX = x;
            _centerY = y;
            _phaseDelta = phaseDelta;
            _amplX = amplX;
            _amplY = amplY;
            _speed = speed;
            _driftAngle = driftAngle;
            _driftSpeed = driftSpeed;
            _time = 0;
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
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _time += dt;

            // Lissajous curve with 3:2 frequency ratio
            // x(t) = A * sin(3t + delta)
            // y(t) = B * sin(2t)
            double lx = _amplX * Math.Sin(3.0 * _speed * _time + _phaseDelta);
            double ly = _amplY * Math.Sin(2.0 * _speed * _time);

            // Drift the centre in a direction, bouncing off screen margins
            _centerX += Math.Cos(_driftAngle) * _driftSpeed * dt;
            _centerY += Math.Sin(_driftAngle) * _driftSpeed * dt;

            double margin = _amplX + 35;
            double marginY = _amplY + 40;
            if (_centerX < margin || _centerX > ScreenWidth - margin)
                _driftAngle = Math.PI - _driftAngle;
            if (_centerY < marginY || _centerY > ScreenHeight * 0.6)
                _driftAngle = -_driftAngle;

            _centerX = Math.Clamp(_centerX, margin, ScreenWidth - margin);
            _centerY = Math.Clamp(_centerY, marginY, ScreenHeight * 0.6);

            PositionX = (int)Math.Clamp(_centerX + lx, 35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(_centerY + ly, 40, ScreenHeight - 150);

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
