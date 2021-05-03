using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Bullet : IMovingObject
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public Texture2D Image { get; set; }
        public bool Active { get; set; }

        public Bullet(int x, int y, Texture2D image)
        {
            Position_X = x;
            Position_Y = y;
            Active = true;
            Image = image;
        }

        public void MoveAuto(GameTime gameTime)
        {
            Position_Y -= 15;

            if (Position_Y < 0) Active = false;
        }

        public void MoveLeft(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void MoveRight(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void MoveUp(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void MoveDown(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
