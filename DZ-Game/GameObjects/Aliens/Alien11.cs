using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien11 : Alien
    {
        private readonly Func<int> _getPlayerX;
        private readonly double _horizontalSpeed;
        private readonly double _verticalAmplitude;
        private readonly double _verticalFrequency;
        private readonly double _fireInterval;
        private readonly double _phase;
        private double _positionX;
        private double _baseY;
        private double _time;
        private double _fireTimer;

        public Alien11(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double horizontalSpeed, double verticalAmplitude, double verticalFrequency,
            double fireInterval, double phase, double initialFireDelay, Func<int> getPlayerX)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _positionX = x;
            _baseY = y;
            _horizontalSpeed = horizontalSpeed;
            _verticalAmplitude = verticalAmplitude;
            _verticalFrequency = verticalFrequency;
            _fireInterval = fireInterval;
            _phase = phase;
            _fireTimer = initialFireDelay;
            _getPlayerX = getPlayerX;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _time += dt;

            int playerX = _getPlayerX?.Invoke() ?? ScreenWidth / 2;
            double distanceX = playerX - _positionX;
            double maxStep = _horizontalSpeed * dt;
            _positionX += Math.Clamp(distanceX, -maxStep, maxStep);

            PositionX = (int)Math.Clamp(_positionX, 35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(
                _baseY + _verticalAmplitude * Math.Sin(_time * _verticalFrequency + _phase),
                40, ScreenHeight * 0.46);

            _fireTimer += dt;
            if (_fireTimer >= _fireInterval)
            {
                WantsToFire = true;
                _fireTimer = 0;
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
