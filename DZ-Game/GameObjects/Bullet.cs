using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;

namespace DZGame.GameObjects
{
    public class Bullet : IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Bullet(int x, int y)
        {
            Position_X = x;
            Position_Y = y;
        }

        public void MoveAuto(GameTime gameTime)
        {
            throw new NotImplementedException();
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
