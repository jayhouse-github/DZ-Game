using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class TitleScreen
    {
        private readonly SpriteFont _font;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public TitleScreen(SpriteFont font, int screenWidth, int screenHeight)
        {
            _font = font;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var text = "DEATH ZONE";
            var textSize = _font.MeasureString(text);
            var x = (_screenWidth - textSize.X) / 2;
            var y = 250;
            spriteBatch.DrawString(_font, text, new Vector2(x, y), Color.Red);
        }
    }
}
