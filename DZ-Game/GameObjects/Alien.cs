using System;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Alien : IMovingObject
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public Texture2D Image { get; set; }
        public bool Active { get; set; }
        public int ExplodeFrameNumber { get; set; }
        public int AlienType { get; set; }
        public int Strength { get; set; }
        public int Velocity { get; set; }

        public Alien(int screenWidth, int screenHeight, float starSpeed, Texture2D image)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Image = image;
            Active = true;
        }

        public void MoveAuto(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void MoveDown(GameTime gameTime)
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
    }
}
