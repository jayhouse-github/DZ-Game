using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DZGame.Enums;
using DZGame.GameObjects;

namespace DZGame.GameObjects
{
    public class SpriteStripAnimation : BaseMovingObject
    {
        private readonly int _frameWidth;
        private readonly int _frameHeight;
        private readonly int _totalFrames;
        private readonly double _frameDuration;
        private double _currentFrameTime;
        private int _currentFrame;

        public SpriteStripAnimation(int x, int y, int frameWidth, int frameHeight, int totalFrames, double frameDurationSeconds, Texture2D textureStrip)
            : base(x, y, 0, textureStrip, 0, 0, MovingObjectType.Explosion)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _totalFrames = totalFrames;
            _frameDuration = frameDurationSeconds;
            _currentFrame = 0;
            _currentFrameTime = 0;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            _currentFrameTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (_currentFrameTime >= _frameDuration)
            {
                _currentFrameTime = 0;
                _currentFrame++;
                if (_currentFrame >= _totalFrames)
                {
                    Active = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Active) return;

            var sourceRect = new Rectangle(_currentFrame * _frameWidth, 0, _frameWidth, _frameHeight);
            spriteBatch.Draw(Image, new Vector2(PositionX, PositionY), sourceRect, Color.White);
        }

        public override void MoveAuto(GameTime gameTime) { }
    }
}