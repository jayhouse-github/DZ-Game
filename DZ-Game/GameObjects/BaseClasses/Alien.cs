using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public abstract class Alien : CollisionObject, IAlien
    {
        public int ExplodeFrame { get; set; }
        public int Type { get; set; }
        public int Strength { get; set; }
        public int Velocity { get; set; }

        protected Alien(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image) 
            : base(x, y, z, image, screenWidth, screenHeight) { }

        public abstract override void MoveAuto(GameTime gameTime);
        
        public override void MoveDown(GameTime gameTime)
        {
            PositionY -= 2;

            if (PositionY < 40)
                PositionY = 40;
        }

        public override void MoveLeft(GameTime gameTime)
        {
            PositionX += 3;

            if (PositionX > ScreenWidth - 35)
                PositionX = ScreenWidth - 35;
        }

        public override void MoveRight(GameTime gameTime)
        {
            PositionX -= 3;

            if (PositionX < 35)
                PositionX = 35;
        }

        public override void MoveUp(GameTime gameTime)
        {
            PositionY += 2;

            if (PositionY > ScreenHeight - 150)
                PositionY = ScreenHeight - 150;
        }
    }
}

