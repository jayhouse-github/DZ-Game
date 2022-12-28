using System;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects.Aliens
{
    public class Alien1 : Alien
    {
        public Alien1(int screenWidth, int screenHeight, Texture2D image)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Image = image;
            Active = true;
        }

        public override void MoveAuto(GameTime gameTime)
        {
            var r = new Random();
            Position_X += r.Next(-12, 12);
            Position_Y += r.Next(-12, 12);

            if (Position_X > _screenWidth - 35)
                Position_X = _screenWidth - 35;

            if (Position_X < 35)
                Position_X = 35;

            if (Position_Y > _screenHeight - 150)
                Position_Y = _screenHeight - 150;

            if (Position_Y < 40)
                Position_Y = 40;
        }
    }
}
