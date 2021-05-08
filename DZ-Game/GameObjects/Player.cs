using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Player : IMovingObject
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public Texture2D Image { get; set; }
        public bool Active { get; set; }

        public Player(int x, int y, int z, int screenWidth, int screenHeight, Texture2D image)
        {
            Position_X = x;
            Position_Y = y;
            Position_Z = z;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Image = image;
            Active = true;
        }

        public void MoveAuto(GameTime gameTime)
        {
            return;
        }

        public void MoveLeft(GameTime gameTime)
        {
            if (Position_X > 35)
            {
                Position_X -= 10;
            }
        }

        public void MoveRight(GameTime gameTime)
        {
            if(Position_X < _screenWidth - 105)
            {
                Position_X += 10;
            }
        }

        public void MoveUp(GameTime gameTime)
        {
            if(Position_Y > _screenHeight - 350)
            {
                Position_Y -= 10;
            }
        }

        public void MoveDown(GameTime gameTime)
        {
            if(Position_Y < _screenHeight - 70)
            {
                Position_Y += 10;
            }
        }
    }
}
