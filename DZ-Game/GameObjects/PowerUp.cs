using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class PowerUp : CollisionObject
    {
        public int RegenValue { get; set; }

        public PowerUp(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image, int regenValue)
            : base(x, y, z, image, screenWidth, screenHeight, MovingObjectType.PowerUp)
        {
            Active = true;
            RegenValue = regenValue;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            PositionY += 5;
            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);

            if (PositionY > ScreenHeight) Active = false;
        }
        
    }
}
