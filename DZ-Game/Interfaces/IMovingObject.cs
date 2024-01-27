using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.Interfaces
{
    public interface IMovingObject
    {
        int PositionX { get; set; }
        int PositionY { get; set; }
        int PositionZ { get; set; }
        Texture2D Image { get; set; }
        public bool Active { get; set; }
        void MoveAuto(GameTime gameTime);
        void MoveLeft(GameTime gameTime);
        void MoveRight(GameTime gameTime);
        void MoveUp(GameTime gameTime);
        void MoveDown(GameTime gameTime);
    }
}
