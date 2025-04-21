using DZGame.Enums;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public abstract class BaseMovingObject : IMovingObject
    {
        public int ScreenWidth;
        public int ScreenHeight;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }
        public bool Active { get; set; }
        public Texture2D Image { get; set; }

        public abstract void MoveAuto(GameTime gameTime);
        
        public virtual void MoveLeft(GameTime gameTime)
        {
            PositionX += 3;
        }

        public virtual void MoveRight(GameTime gameTime)
        {
            PositionX -= 3;
        }

        public virtual void MoveUp(GameTime gameTime)
        {
            PositionY += 2;
        }

        public virtual void MoveDown(GameTime gameTime)
        {
            PositionY -= 2;
        }
        public MovingObjectType MoveType { get; set; }

        public BaseMovingObject(int x, int y, int z, Texture2D image, int screenWidth, int screenHeight, MovingObjectType type)
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
            Image = image;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            MoveType = type;
        }
    }
}