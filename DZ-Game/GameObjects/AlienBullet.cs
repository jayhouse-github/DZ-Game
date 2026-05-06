using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class AlienBullet : CollisionObject
    {
        public bool Destroyable { get; set; }
        public int Damage { get; set; }

        public AlienBullet(int x, int y, int z,int screenWidth, int screenHeight, Texture2D image, bool destroyable, int damage)
            : base(x, y, z, image, screenWidth, screenHeight, MovingObjectType.AlienBullet)
        {
            Active = true;
            Destroyable = destroyable;
            Damage = damage;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            PositionY += 15;
            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);

            if (PositionY > ScreenHeight) Active = false;
        }
        
    }
}
