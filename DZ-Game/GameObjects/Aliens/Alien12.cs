using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien12 : Alien
    {
        private readonly int _row;
        private readonly int _column;
        private readonly int _columnCount;
        private readonly int _rowCount;
        private readonly bool _isVolleyShooter;
        private readonly double _fireInterval;
        private double _nextFireThreshold;
        private double _time;
        private double _fireTimer;

        private const double SweepSpeed = 0.65;
        private const double FormationVerticalAmplitude = 55.0;
        private const double FormationVerticalFrequency = 0.7;
        private const double ColumnWaveAmplitude = 13.0;
        private const double RowShearAmplitude = 20.0;
        private const double FormationTop = 95.0;
        private const double RowSpacing = 65.0;
        private const double ColumnSpacing = 78.0;

        public Alien12(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            int row, int column, int rowCount, int columnCount,
            bool isVolleyShooter, double fireInterval, double initialFireDelay)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _row = row;
            _column = column;
            _rowCount = rowCount;
            _columnCount = columnCount;
            _isVolleyShooter = isVolleyShooter;
            _fireInterval = fireInterval;
            _nextFireThreshold = initialFireDelay;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _time += dt;

            double columnSpacing = Math.Min(ColumnSpacing, (ScreenWidth - 140.0) / Math.Max(1, _columnCount - 1));
            double formationWidth = (_columnCount - 1) * columnSpacing * 1.1 + RowShearAmplitude * 2;
            double sweepRange = Math.Max(0, (ScreenWidth - 140.0 - formationWidth) / 2.0);
            double sweep = Math.Sin(_time * SweepSpeed);
            double formationCenterX = ScreenWidth / 2.0 + sweep * sweepRange;
            double rowPhase = _row * (2.0 * Math.PI / Math.Max(1, _rowCount));
            double columnPhase = _time * 1.45 - _column * 0.8 + rowPhase;
            double expansion = 1.0 + 0.1 * Math.Sin(_time * 0.75);
            double columnOffset = (_column - (_columnCount - 1) / 2.0) * columnSpacing * expansion;
            double rowY = FormationTop + _row * RowSpacing
                + FormationVerticalAmplitude * Math.Sin(_time * FormationVerticalFrequency)
                + ColumnWaveAmplitude * Math.Cos(columnPhase);
            double rowShear = RowShearAmplitude * Math.Sin(_time * 0.9 + rowPhase);

            PositionX = (int)Math.Clamp(
                formationCenterX + columnOffset + rowShear,
                35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(
                rowY, 40, ScreenHeight * 0.55);

            if (_isVolleyShooter)
            {
                _fireTimer += dt;
                if (_fireTimer >= _nextFireThreshold)
                {
                    WantsToFire = true;
                    _fireTimer = 0;
                    _nextFireThreshold = _fireInterval;
                }
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
