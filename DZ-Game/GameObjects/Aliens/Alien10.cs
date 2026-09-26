using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien10 : Alien
    {
        private enum AttackState { Orbiting, Diving, Recovering }

        private AttackState _state;
        private readonly Func<int> _getPlayerX;
        private readonly Random _rng;
        private readonly double _orbitRadiusX;
        private readonly double _orbitRadiusY;
        private readonly double _orbitSpeed;
        private double _centerX;
        private double _centerY;
        private double _orbitAngle;
        private double _roamDuration;
        private double _stateTimer;
        private double _diveVX;
        private double _fireTimer;

        private const double DiveSpeedY = 400.0;
        private const double RecoverSpeedY = -480.0;
        private const double FireInterval = 1.1;

        public Alien10(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double orbitRadiusX, double orbitRadiusY, double orbitSpeed, double initialAngle,
            double initialRoamDuration, Func<int> getPlayerX, int rngSeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _centerX = x;
            _centerY = y;
            _orbitRadiusX = orbitRadiusX;
            _orbitRadiusY = orbitRadiusY;
            _orbitSpeed = orbitSpeed;
            _orbitAngle = initialAngle;
            _roamDuration = initialRoamDuration;
            _getPlayerX = getPlayerX;
            _rng = new Random(rngSeed);
            _state = AttackState.Orbiting;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _stateTimer += dt;

            switch (_state)
            {
                case AttackState.Orbiting:
                    _orbitAngle += _orbitSpeed * dt;
                    PositionX = (int)Math.Clamp(_centerX + _orbitRadiusX * Math.Cos(_orbitAngle), 35, ScreenWidth - 35);
                    PositionY = (int)Math.Clamp(_centerY + _orbitRadiusY * Math.Sin(_orbitAngle), 40, ScreenHeight * 0.42);

                    if (_stateTimer >= _roamDuration)
                        BeginDive();
                    break;

                case AttackState.Diving:
                    PositionX = (int)Math.Clamp(PositionX + _diveVX * dt, 35, ScreenWidth - 35);
                    PositionY = Math.Min(PositionY + (int)(DiveSpeedY * dt), ScreenHeight - 120);

                    _fireTimer += dt;
                    if (_fireTimer >= FireInterval)
                    {
                        WantsToFire = true;
                        _fireTimer = 0;
                    }

                    if (PositionY >= ScreenHeight - 120)
                    {
                        _state = AttackState.Recovering;
                        _stateTimer = 0;
                        _fireTimer = 0;
                    }
                    break;

                case AttackState.Recovering:
                    PositionY = Math.Max(PositionY + (int)(RecoverSpeedY * dt), (int)_centerY);
                    PositionX = (int)Math.Clamp(
                        PositionX + (_centerX - PositionX) * Math.Min(1.0, dt * 4.0),
                        35, ScreenWidth - 35);

                    if (PositionY <= _centerY)
                    {
                        _orbitAngle = Math.Atan2(
                            (PositionY - _centerY) / _orbitRadiusY,
                            (PositionX - _centerX) / _orbitRadiusX);
                        _roamDuration = 1.8 + _rng.NextDouble() * 1.2;
                        _state = AttackState.Orbiting;
                        _stateTimer = 0;
                    }
                    break;
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }

        private void BeginDive()
        {
            _state = AttackState.Diving;
            _stateTimer = 0;
            _fireTimer = 0;

            int playerX = _getPlayerX?.Invoke() ?? ScreenWidth / 2;
            double diveTime = (ScreenHeight - 120 - PositionY) / DiveSpeedY;
            _diveVX = diveTime > 0 ? (playerX - PositionX) / diveTime : 0;
            _diveVX = Math.Clamp(_diveVX, -260, 260);
        }
    }
}
