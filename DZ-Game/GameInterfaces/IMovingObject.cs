using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameInterfaces
{
    public interface IMovingObject
    {
        int Position_X { get; set; }
        int Position_Y { get; set; }
        int Position_Z { get; set; }
        Texture2D Image { get; set; }

        void MoveAuto(GameTime gameTime);
        void MoveLeft(GameTime gameTime);
        void MoveRight(GameTime gameTime);
        void MoveUp(GameTime gameTime);
        void MoveDown(GameTime gameTime);
    }
}
