using System;
using DZGame.GameInterfaces;
using Microsoft.Xna.Framework;

namespace DZGame.GameObjects
{
    public class Player :IMovingObject
    {
        public int Position_X { get; set; }
        public int Position_Y { get; set; }
        public int Position_Z { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }

        public Player(int x, int y, int z, int screenWidth, int screenHeight)
        {
            Position_X = x;
            Position_Y = y;
            Position_Z = z;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
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
            if(Position_X < ScreenWidth - 105)
            {
                Position_X += 10;
            }
        }

        public void MoveUp(GameTime gameTime)
        {
            if(Position_Y > ScreenHeight - 350)
            {
                Position_Y -= 10;
            }
        }

        public void MoveDown(GameTime gameTime)
        {
            if(Position_Y < ScreenHeight - 70)
            {
                Position_Y += 10;
            }
        }
    }
}
