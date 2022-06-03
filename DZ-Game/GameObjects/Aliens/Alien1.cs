using System;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects.Aliens
{
    public class Alien1 : Alien
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public Alien1(int screenWidth, int screenHeight, Texture2D image)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Image = image;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
