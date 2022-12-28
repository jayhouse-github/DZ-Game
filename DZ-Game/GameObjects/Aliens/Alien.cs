using System;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects.Aliens
{
    public abstract class Alien : IMovingObject, IAlien
    {
        public int _screenWidth;
        public int _screenHeight;
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public Texture2D Image { get; set; }
        public bool Active { get; set; }
        public int ExplodeFrame { get; set; }
        public int Type { get; set; }
        public int Strength { get; set; }
        public int Velocity { get; set; }

        public abstract void MoveAuto(GameTime gameTime);
        
        public void MoveDown(GameTime gameTime)
        {
            Position_Y -= 2;

            if (Position_Y < 40)
                Position_Y = 40;
        }

        public void MoveLeft(GameTime gameTime)
        {
            Position_X += 3;

            if (Position_X > _screenWidth - 35)
                Position_X = _screenWidth - 35;
        }

        public void MoveRight(GameTime gameTime)
        {
            Position_X -= 3;

            if (Position_X < 35)
                Position_X = 35;
        }

        public void MoveUp(GameTime gameTime)
        {
            Position_Y += 2;

            if (Position_Y > _screenHeight - 150)
                Position_Y = _screenHeight - 150;
        }
    }
}

