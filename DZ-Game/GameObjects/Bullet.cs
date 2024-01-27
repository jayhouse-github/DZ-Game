using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Bullet : CollisionObject
    {
        public Bullet(int x, int y, int z, Texture2D image) 
            : base(x, y, z, image, 0, 0)
        {
            Active = true;
        }

        public override  void MoveAuto(GameTime gameTime)
        {
            PositionY -= 15;

            if (PositionY < 0) Active = false;
        }

        public override void MoveLeft(GameTime gameTime) { }

        public override void MoveRight(GameTime gameTime) { }

        public override  void MoveUp(GameTime gameTime) { }

        public override  void MoveDown(GameTime gameTime) { }
    }
}
