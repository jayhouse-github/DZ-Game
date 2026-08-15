using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien6: Fast elliptical roamer that periodically locks on to the player's X position
    // and dives straight down in a swoop attack, then loops back up off the top of the screen.
    // Each alien has its own orbit speed, radius, and swoop timing so the attacks stagger naturally.
    public class Alien6 : Alien
    {
        private enum SwoopState { Roaming, Swooping, Recovering }

        private SwoopState _state;
        private double _stateTimer;
        private double _roamDuration;   // seconds before next swoop

        // Roaming — elliptical orbit around a centre point
        private double _orbitCX;
        private double _orbitCY;
        private double _orbitAngle;
        private double _orbitRadiusX;
        private double _orbitRadiusY;
        private double _orbitSpeed;     // radians per second, negative = clockwise

        // Swoop — straight fast dive aimed at player X at swoop-start
        private double _swoopVX;        // px/s horizontal (toward locked player X)
        private const double SwoopVY = 420.0; // px/s downward

        // Recovering — curves back up from below the screen
        private const double RecoverVY = -500.0; // px/s upward

        private readonly Func<int> _getPlayerX;
        private readonly Random _rng;

        public Alien6(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double orbitRadiusX, double orbitRadiusY, double orbitSpeed,
            double initialAngle, double initialRoamDuration, Func<int> getPlayerX, int rngSeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _orbitCX = x;
            _orbitCY = y;
            _orbitRadiusX = orbitRadiusX;
            _orbitRadiusY = orbitRadiusY;
            _orbitSpeed = orbitSpeed;
            _orbitAngle = initialAngle;
            _roamDuration = initialRoamDuration;
            _getPlayerX = getPlayerX;
            _rng = new Random(rngSeed);
            _state = SwoopState.Roaming;
            _stateTimer = 0;
            Active = true;
        }

        public override void MoveLeft(GameTime gameTime)
        {
            _orbitCX += 3;
            if (_orbitCX > ScreenWidth - 35) _orbitCX = ScreenWidth - 35;
        }

        public override void MoveRight(GameTime gameTime)
        {
            _orbitCX -= 3;
            if (_orbitCX < 35) _orbitCX = 35;
        }

        public override void MoveUp(GameTime gameTime)
        {
            _orbitCY += 2;
            if (_orbitCY > ScreenHeight * 0.3) _orbitCY = ScreenHeight * 0.3;
        }

        public override void MoveDown(GameTime gameTime)
        {
            _orbitCY -= 2;
            if (_orbitCY < 40) _orbitCY = 40;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _stateTimer += dt;

            switch (_state)
            {
                case SwoopState.Roaming:
                    _orbitAngle += _orbitSpeed * dt;

                    PositionX = (int)(_orbitCX + _orbitRadiusX * Math.Cos(_orbitAngle));
                    PositionY = (int)(_orbitCY + _orbitRadiusY * Math.Sin(_orbitAngle));

                    PositionX = Math.Clamp(PositionX, 35, ScreenWidth - 35);
                    PositionY = Math.Clamp(PositionY, 40, (int)(ScreenHeight * 0.35));

                    if (_stateTimer >= _roamDuration)
                        BeginSwoop();
                    break;

                case SwoopState.Swooping:
                    PositionX = (int)Math.Clamp(PositionX + _swoopVX * dt, 35, ScreenWidth - 35);
                    PositionY += (int)(SwoopVY * dt);

                    // Transition to recovery once past the safe player zone
                    if (PositionY >= ScreenHeight - 120)
                    {
                        _state = SwoopState.Recovering;
                        _stateTimer = 0;
                    }
                    break;

                case SwoopState.Recovering:
                    PositionY += (int)(RecoverVY * dt);
                    // Drift X back toward orbit centre so there's no position snap on re-entry
                    PositionX = (int)Math.Clamp(PositionX + (_orbitCX - PositionX) * Math.Min(1.0, dt * 5.0), 35, ScreenWidth - 35);

                    // Once clear back into the upper roaming band, re-anchor orbit and resume
                    if (PositionY <= _orbitCY + (int)_orbitRadiusY + 10)
                    {
                        // Smoothly pick up orbit angle from current position
                        _orbitAngle = Math.Atan2(
                            (PositionY - _orbitCY) / _orbitRadiusY,
                            (PositionX - _orbitCX) / _orbitRadiusX);
                        _roamDuration = 1.8 + _rng.NextDouble() * 2.4; // 1.8–4.2 s
                        _state = SwoopState.Roaming;
                        _stateTimer = 0;
                    }
                    break;
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }

        private void BeginSwoop()
        {
            _state = SwoopState.Swooping;
            _stateTimer = 0;

            int playerX = _getPlayerX?.Invoke() ?? ScreenWidth / 2;
            double dx = playerX - PositionX;

            // Compute X velocity so the alien reaches player X roughly when it hits the bottom
            double swoopTimeEstimate = (ScreenHeight - 120 - PositionY) / SwoopVY;
            _swoopVX = swoopTimeEstimate > 0 ? dx / swoopTimeEstimate : 0;

            // Cap X speed so a player who moves has a genuine chance to dodge
            _swoopVX = Math.Clamp(_swoopVX, -280, 280);
        }
    }
}
