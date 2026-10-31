using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien14 : Alien
    {
        private readonly Vector2[] _waypoints;
        private readonly double[] _segmentStart;
        private readonly double _trackLength;
        private readonly double _speed;
        private readonly int[] _gunPortSegments;
        private readonly double _fireCooldown;
        private double _time;
        private double _distance;
        private double _fireTimer;
        private int _segmentIndex;
        private readonly Func<int> _getPlayerX;
        private readonly double _followSpeed;
        private readonly double _maxFollowOffset;
        private double _followOffset;

        // Whole chain shares the same clock, so the surge never breaks the spacing between aliens
        private const double SurgeAmplitude = 0.45;
        private const double SurgeSpeed = 2.4;

        public Alien14(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image,
            int strength, int scoreValue, bool bulletsDestroyable, int shieldStrength,
            Vector2[] waypoints, double speed, double startOffset, int[] gunPortSegments, double fireCooldown,
            Func<int> getPlayerX = null, double followSpeed = 0, double maxFollowOffset = 0)
            : base(x, y, z, screenWidth, screenHeight, image, strength, scoreValue, bulletsDestroyable, shieldStrength)
        {
            _waypoints = waypoints;
            _speed = speed;
            _gunPortSegments = gunPortSegments;
            _fireCooldown = fireCooldown;
            _getPlayerX = getPlayerX;
            _followSpeed = followSpeed;
            _maxFollowOffset = maxFollowOffset;

            // Cumulative distance at the start of each segment of the closed track
            _segmentStart = new double[waypoints.Length];
            double total = 0;
            for (int i = 0; i < waypoints.Length; i++)
            {
                _segmentStart[i] = total;
                total += Vector2.Distance(waypoints[i], waypoints[(i + 1) % waypoints.Length]);
            }
            _trackLength = total;

            _distance = startOffset % _trackLength;
            UpdatePosition();
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            double dt = gameTime.ElapsedGameTime.TotalSeconds;
            _time += dt;
            _fireTimer += dt;

            double surge = 1.0 + SurgeAmplitude * Math.Sin(_time * SurgeSpeed);
            _distance = (_distance + _speed * surge * dt) % _trackLength;

            // Slide the whole track sideways to hunt the player; every alien on the track gets the
            // same inputs each frame, so the chain stays in formation
            if (_getPlayerX != null && _followSpeed > 0)
            {
                double target = Math.Clamp(_getPlayerX() - ScreenWidth / 2.0, -_maxFollowOffset, _maxFollowOffset);
                double step = _followSpeed * dt;
                double delta = target - _followOffset;
                _followOffset += Math.Abs(delta) <= step ? delta : Math.Sign(delta) * step;
            }

            int previousSegment = _segmentIndex;
            UpdatePosition();

            // Fire on turning into a gun-port segment, giving rhythmic bursts at fixed corners
            if (_segmentIndex != previousSegment
                && Array.IndexOf(_gunPortSegments, _segmentIndex) >= 0
                && _fireTimer >= _fireCooldown)
            {
                WantsToFire = true;
                _fireTimer = 0;
            }

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }

        private void UpdatePosition()
        {
            int segment = _waypoints.Length - 1;
            while (segment > 0 && _segmentStart[segment] > _distance)
                segment--;
            _segmentIndex = segment;

            Vector2 from = _waypoints[segment];
            Vector2 to = _waypoints[(segment + 1) % _waypoints.Length];
            double segmentLength = Vector2.Distance(from, to);
            float t = segmentLength > 0 ? (float)((_distance - _segmentStart[segment]) / segmentLength) : 0f;
            Vector2 position = Vector2.Lerp(from, to, t);

            PositionX = (int)Math.Clamp(position.X + _followOffset, 35, ScreenWidth - 35);
            PositionY = (int)Math.Clamp(position.Y, 40, ScreenHeight - 150);
        }
    }
}
