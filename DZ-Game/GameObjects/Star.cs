using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class Star : IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        private float starSpeedMultiplier { get; set; }
        private float StarSpeed { get; set; }
        public Texture2D Image { get; set; }

        public Star(int screenWidth, int screenHeight, float starSpeed, Texture2D image)
        {
            //TODO - move private variables to fields.
            var r = new Random();
            Position_X = r.Next(screenWidth);
            Position_Y = r.Next(screenHeight);
            Position_Z = r.Next(1, 4);
            StarSpeed = starSpeed;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            starSpeedMultiplier = StarSpeed * Position_Z;
            Image = image;
        }

        public void MoveAuto(GameTime gameTime)
        {
            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_Y > ScreenHeight)
            {
                Position_Y = 0;
            }
        }

        public void MoveLeft(GameTime gameTime)
        {
            Position_X += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X > ScreenWidth)
            {
                Position_X = 0;
            }
        }

        public void MoveRight(GameTime gameTime)
        {
            Position_X -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Position_X < 0)
            {
                Position_X = ScreenWidth;
            }
        }

        public void MoveUp(GameTime gameTime)
        {
            Position_Y += (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void MoveDown(GameTime gameTime)
        {
            Position_Y -= (int)(starSpeedMultiplier * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }
}
