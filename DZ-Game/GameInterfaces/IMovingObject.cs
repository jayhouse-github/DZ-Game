using System;
using Microsoft.Xna.Framework;

namespace DZGame.GameInterfaces
{
    public interface IMovingObject
    {
        int Position_X { get; set; }
        int Position_Y { get; set; }
        int Position_Z { get; set; }

        void MoveAuto(GameTime gameTime);
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
    }
}
