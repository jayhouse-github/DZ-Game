using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class TitleScreen
    {
        private readonly Texture2D _pixelTexture;
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public TitleScreen(Texture2D pixelTexture, int screenWidth, int screenHeight)
        {
            _pixelTexture = pixelTexture;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw title text using simple rectangles
            DrawText(spriteBatch, "DEATH ZONE", 200, 150, 6, Color.Red);
            DrawText(spriteBatch, "[Placeholder text - to be filled in later]", 150, 350, 2, Color.Gray);
            DrawText(spriteBatch, "PRESS FIRE TO START", 250, 550, 3, Color.White);
        }

        private void DrawText(SpriteBatch spriteBatch, string text, int x, int y, int size, Color color)
        {
            int charX = x;
            foreach (char c in text.ToUpper())
            {
                DrawChar(spriteBatch, c, charX, y, size, color);
                charX += size * 8; // Character width + spacing
            }
        }

        private void DrawChar(SpriteBatch spriteBatch, char c, int x, int y, int size, Color color)
        {
            // Simple pixel font - draw basic characters using rectangles
            int pixelSize = size;

            switch (c)
            {
                case 'D':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y + pixelSize, pixelSize, pixelSize * 5, color); // Right vertical
                    break;
                case 'E':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 3, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    break;
                case 'A':
                    DrawRect(spriteBatch, x, y + pixelSize, pixelSize, pixelSize * 6, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y + pixelSize, pixelSize, pixelSize * 6, color); // Right vertical
                    break;
                case 'T':
                    DrawRect(spriteBatch, x, y, pixelSize * 6, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize * 2, y + pixelSize, pixelSize, pixelSize * 6, color); // Center vertical
                    break;
                case 'H':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y, pixelSize, pixelSize * 7, color); // Right vertical
                    break;
                case 'Z':
                    DrawRect(spriteBatch, x, y, pixelSize * 6, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle diagonal
                    DrawRect(spriteBatch, x, y + pixelSize * 6, pixelSize * 6, pixelSize, color); // Bottom horizontal
                    break;
                case 'O':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y, pixelSize, pixelSize * 7, color); // Right vertical
                    break;
                case 'N':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 2, pixelSize * 4, pixelSize, color); // Diagonal
                    DrawRect(spriteBatch, x + pixelSize * 5, y, pixelSize, pixelSize * 7, color); // Right vertical
                    break;
                case 'P':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y, pixelSize, pixelSize * 3, color); // Right top vertical
                    break;
                case 'R':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y, pixelSize, pixelSize * 3, color); // Right top vertical
                    DrawRect(spriteBatch, x + pixelSize * 5, y + pixelSize * 4, pixelSize, pixelSize * 3, color); // Right bottom vertical
                    break;
                case 'S':
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x, y + pixelSize, pixelSize, pixelSize * 2, color); // Top left vertical
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 4, pixelSize, color); // Middle horizontal
                    DrawRect(spriteBatch, x + pixelSize * 5, y + pixelSize * 4, pixelSize, pixelSize * 2, color); // Bottom right vertical
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    break;
                case 'F':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 3, pixelSize * 3, pixelSize, color); // Middle horizontal
                    break;
                case 'I':
                    DrawRect(spriteBatch, x, y, pixelSize * 4, pixelSize, color); // Top horizontal
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize, pixelSize, pixelSize * 5, color); // Center vertical
                    DrawRect(spriteBatch, x, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    break;
                case 'L':
                    DrawRect(spriteBatch, x, y, pixelSize, pixelSize * 7, color); // Left vertical
                    DrawRect(spriteBatch, x + pixelSize, y + pixelSize * 6, pixelSize * 4, pixelSize, color); // Bottom horizontal
                    break;
                case '[':
                case ']':
                case '-':
                case '.':
                    // Simple placeholder for special chars
                    DrawRect(spriteBatch, x + pixelSize * 2, y + pixelSize * 3, pixelSize, pixelSize, color);
                    break;
                case ' ':
                    // Space - no drawing
                    break;
                default:
                    // Unknown character - draw a small square
                    DrawRect(spriteBatch, x + pixelSize * 2, y + pixelSize * 3, pixelSize, pixelSize, color);
                    break;
            }
        }

        private void DrawRect(SpriteBatch spriteBatch, int x, int y, int width, int height, Color color)
        {
            spriteBatch.Draw(_pixelTexture, new Rectangle(x, y, width, height), color);
        }
    }
}
