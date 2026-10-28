using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien13 : Alien
    {
        private enum SlingState { InFormation, Slung, Returning }

        private SlingState _state;
        private readonly Random _rng;
        private readonly double _ringRadius;
        private readonly double _ringSpeed;
        private readonly double _slotAngle;
        private double _time;
        private double _stateTimer;
        private double _slingDelay;
        private double _posX;
        private double _posY;
        private double _velX;
        private double _velY;
        private bool _hasFired;

        // Shared hub path - every alien in the formation orbits the same moving hub
        private const double HubCenterY = 230.0;
        private const double HubRadiusX = 180.0;
        private const double HubRadiusY = 30.0;
        private const double HubSpeed = 0.5;
        private const double BreatheAmplitude = 0.15;
        private const double BreatheSpeed = 1.2;

        private const double SlingSpeed = 380.0;
        private const double SlingDuration = 1.0;
        private const double ReturnSpeed = 450.0;
        private const double DockDistance = 8.0;

        public Alien13(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double ringRadius, double ringSpeed, double slotAngle, double initialSlingDelay, int rngSeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _ringRadius = ringRadius;
            _ringSpeed = ringSpeed;
            _slotAngle = slotAngle;
            _slingDelay = initialSlingDelay;
            _rng = new Random(rngSeed);
            _state = SlingState.InFormation;

            GetSlotPosition(out _posX, out _posY);
            PositionX = (int)_posX;
            PositionY = (int)_posY;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _time += dt;
            _stateTimer += dt;

            GetSlotPosition(out double slotX, out double slotY);

            switch (_state)
            {
                case SlingState.InFormation:
                    _posX = slotX;
                    _posY = slotY;

                    if (_stateTimer >= _slingDelay)
                        BeginSling();
                    break;

                case SlingState.Slung:
                    _posX += _velX * dt;
                    _posY += _velY * dt;

                    // Bounce off the sides and the bottom of the alien play area
                    if (_posX < 35 || _posX > ScreenWidth - 35)
                    {
                        _velX = -_velX;
                        _posX = Math.Clamp(_posX, 35, ScreenWidth - 35);
                    }
                    if (_posY < 40 || _posY > ScreenHeight - 150)
                    {
                        _velY = -_velY;
                        _posY = Math.Clamp(_posY, 40, ScreenHeight - 150);
                    }

                    if (!_hasFired && _posY >= ScreenHeight * 0.45)
                    {
                        WantsToFire = true;
                        _hasFired = true;
                    }

                    if (_stateTimer >= SlingDuration)
                    {
                        _state = SlingState.Returning;
                        _stateTimer = 0;
                    }
                    break;

                case SlingState.Returning:
                    double dx = slotX - _posX;
                    double dy = slotY - _posY;
                    double distance = Math.Sqrt(dx * dx + dy * dy);
                    double step = ReturnSpeed * dt;

                    if (distance <= Math.Max(DockDistance, step))
                    {
                        _posX = slotX;
                        _posY = slotY;
                        _state = SlingState.InFormation;
                        _stateTimer = 0;
                        _slingDelay = 8.0 + _rng.NextDouble() * 4.0;
                    }
                    else
                    {
                        _posX += dx / distance * step;
                        _posY += dy / distance * step;
                    }
                    break;
            }

            PositionX = (int)_posX;
            PositionY = (int)_posY;

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }

        private void GetSlotPosition(out double x, out double y)
        {
            double hubX = ScreenWidth / 2.0 + HubRadiusX * Math.Cos(_time * HubSpeed);
            double hubY = HubCenterY + HubRadiusY * Math.Sin(_time * HubSpeed);
            double radius = _ringRadius * (1.0 + BreatheAmplitude * Math.Sin(_time * BreatheSpeed));
            double angle = _slotAngle + _ringSpeed * _time;

            x = Math.Clamp(hubX + radius * Math.Cos(angle), 35, ScreenWidth - 35);
            y = Math.Clamp(hubY + radius * Math.Sin(angle), 40, ScreenHeight * 0.6);
        }

        private void BeginSling()
        {
            // Fling off along the ring's tangent, in the direction the ring is spinning
            double angle = _slotAngle + _ringSpeed * _time;
            double direction = Math.Sign(_ringSpeed);
            _velX = -Math.Sin(angle) * direction * SlingSpeed;
            _velY = Math.Cos(angle) * direction * SlingSpeed;

            // Always attack towards the player
            if (_velY < 0)
                _velY = -_velY;

            _hasFired = false;
            _state = SlingState.Slung;
            _stateTimer = 0;
        }
    }
}
