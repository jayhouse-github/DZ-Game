using System;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects.Aliens
{
    public abstract class Alien : IMovingObject, IAlien
    {
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
            return;
        }

        public void MoveLeft(GameTime gameTime)
        {
            return;
        }

        public void MoveRight(GameTime gameTime)
        {
            return;
        }

        public void MoveUp(GameTime gameTime)
        {
            return;
        }
    }
}

