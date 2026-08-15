using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    // Alien9: Dive bomber. Drifts horizontally at the top of the screen, then locks on
    // the player's X position and dives fast. While diving, sets WantsToFire every
    // FireInterval seconds — DzGame spawns an undestroyable bullet each time.
    // X velocity during the dive is capped so the player has a genuine chance to dodge.
    // Takes only one hit to kill.
    public class Alien9 : Alien
    {
        private enum DiveState { Roaming, Diving, Recovering }

        private DiveState _state;
        private double _stateTimer;
        private double _roamDuration;

        // Roaming
        private double _posX;
        private double _roamVX;   // px/s — gentle horizontal drift
        private const double RoamY = 80.0;

        // Diving
        private double _diveVX;           // computed at dive start
        private const double DiveVY = 380.0;   // px/s downward
        private double _fireTimer;
        private const double FireInterval = 0.4;  // seconds between bullets

        // Recovering
        private const double RecoverVY = -480.0;  // px/s upward

        private readonly Func<int> _getPlayerX;
        private readonly Random _rng;

        public Alien9(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            double roamVX, double initialRoamDuration, Func<int> getPlayerX, int rngSeed)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _posX = x;
            _roamVX = roamVX;
            _roamDuration = initialRoamDuration;
            _getPlayerX = getPlayerX;
            _rng = new Random(rngSeed);
            _state = DiveState.Roaming;
            _stateTimer = 0;
            _fireTimer = 0;
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
            _stateTimer += dt;

            switch (_state)
            {
                case DiveState.Roaming:
                    _posX += _roamVX * dt;
                    if (_posX <= 35) { _posX = 35; _roamVX = Math.Abs(_roamVX); }
                    if (_posX >= ScreenWidth - 35) { _posX = ScreenWidth - 35; _roamVX = -Math.Abs(_roamVX); }

                    PositionX = (int)_posX;
                    PositionY = (int)RoamY;

                    if (_stateTimer >= _roamDuration)
                        BeginDive();
                    break;

                case DiveState.Diving:
                    _posX = Math.Clamp(_posX + _diveVX * dt, 35, ScreenWidth - 35);
                    PositionY += (int)(DiveVY * dt);
                    PositionX = (int)_posX;

                    // Signal the game loop to spawn a bullet
                    _fireTimer += dt;
                    if (_fireTimer >= FireInterval)
                    {
                        WantsToFire = true;
                        _fireTimer = 0;
                    }

                    if (PositionY >= ScreenHeight - 110)
                    {
                        WantsToFire = false;
                        _fireTimer = 0;
                        _state = DiveState.Recovering;
                        _stateTimer = 0;
                    }
                    break;

                case DiveState.Recovering:
                    PositionY += (int)(RecoverVY * dt);
                    PositionX = (int)_posX;

                    if (PositionY <= (int)RoamY)
                    {
                        PositionY = (int)RoamY;
                        _roamDuration = 1.5 + _rng.NextDouble() * 2.5; // 1.5–4.0 s
                        _state = DiveState.Roaming;
                        _stateTimer = 0;
                        _fireTimer = 0;
                    }
                    break;
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }

        private void BeginDive()
        {
            _state = DiveState.Diving;
            _stateTimer = 0;
            _fireTimer = 0;

            int playerX = _getPlayerX?.Invoke() ?? ScreenWidth / 2;
            double dx = playerX - _posX;
            double swoopTime = (ScreenHeight - 110 - RoamY) / DiveVY;
            _diveVX = swoopTime > 0 ? dx / swoopTime : 0;
            _diveVX = Math.Clamp(_diveVX, -250, 250); // cap so player can dodge
        }
    }
}
