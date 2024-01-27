using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Player : CollisionObject, IMovingObject
    {
        public Player(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image) : base(x, y, z, image, screenWidth, screenHeight)
        {
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime) { }

        public override void MoveLeft(GameTime gameTime)
        {
            if (PositionX > 35)
            {
                PositionX -= 10;
            }
        }

        public override void MoveRight(GameTime gameTime)
        {
            if(PositionX < ScreenWidth - 105)
            {
                PositionX += 10;
            }
        }

        public override void MoveUp(GameTime gameTime)
        {
            if(PositionY > ScreenHeight - 350)
            {
                PositionY -= 10;
            }
        }

        public override void MoveDown(GameTime gameTime)
        {
            if(PositionY < ScreenHeight - 70)
            {
                PositionY += 10;
            }
        }
    }
}
