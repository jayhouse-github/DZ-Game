using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Bullet : CollisionObject
    {
        public Bullet(int x, int y, int z, Texture2D image) 
            : base(x, y, z, image, 0, 0, MovingObjectType.PlayerBullet)
        {
            Active = true;
        }

        public override  void MoveAuto(GameTime gameTime)
        {
            PositionY -= 15;
            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);

            if (PositionY < 0) Active = false;
        }

        public override void MoveLeft(GameTime gameTime) { }

        public override void MoveRight(GameTime gameTime) { }

        public override  void MoveUp(GameTime gameTime) { }

        public override  void MoveDown(GameTime gameTime) { }
    }
}
