using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = System.Drawing.Rectangle;

namespace DZGame.GameObjects
{
    public class Alien1 : Alien
    {
        public Alien1(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image) 
            : base (x, y, z, screenWidth, screenHeight, image)
        {
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            var r = new Random();
            PositionX += r.Next(-12, 12);
            PositionY += r.Next(-12, 12);

            if (PositionX > ScreenWidth - 35)
                PositionX = ScreenWidth - 35;

            if (PositionX < 35)
                PositionX = 35;

            if (PositionY > ScreenHeight - 150)
                PositionY = ScreenHeight - 150;

            if (PositionY < 40)
                PositionY = 40;

            CollisionRectangle = new Rectangle(PositionX, PositionY, Image.Width, Image.Height);
        }
    }
}
