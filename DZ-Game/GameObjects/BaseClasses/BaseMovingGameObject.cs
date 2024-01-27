using DZGame.Interfaces;
using Microsoft.Xna.Framework;

namespace DZGame.GameObjects;
using Microsoft.Xna.Framework.Graphics;

public abstract class BaseMovingObject : IMovingObject
{
    public int ScreenWidth;
    public int ScreenHeight;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int PositionZ { get; set; }
    public bool Active { get; set; }
    public Texture2D Image { get; set; }
    
    public abstract void MoveAuto(GameTime gameTime);
    public abstract void MoveLeft(GameTime gameTime);
    public abstract void MoveRight(GameTime gameTime);
    public abstract void MoveUp(GameTime gameTime);
    public abstract void MoveDown(GameTime gameTime);
    
    public BaseMovingObject(int x, int y, int z, Texture2D image, int screenWidth, int screenHeight)
    {
        PositionX = x;
        PositionY = y;
        PositionZ = z;
        Image = image;
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
    }
}