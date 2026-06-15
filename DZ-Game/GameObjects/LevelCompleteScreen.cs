using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class LevelCompleteScreen
    {
        private readonly SpriteFont _font;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public LevelCompleteScreen(SpriteFont font, int screenWidth, int screenHeight)
        {
            _font = font;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var text = "Nice one, level complete";
            var textSize = _font.MeasureString(text);
            var x = (_screenWidth - textSize.X) / 2;
            var y = (_screenHeight - textSize.Y) / 2;
            spriteBatch.DrawString(_font, text, new Vector2(x, y), Color.Lime);
        }
    }
}

