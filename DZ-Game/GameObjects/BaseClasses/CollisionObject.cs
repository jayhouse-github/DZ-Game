using System.Drawing;
using DZGame.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects;

public abstract class CollisionObject : BaseMovingObject
{
    public Rectangle CollisionRectangle { get; set; }
    
    protected CollisionObject(int x, int y, int z, Texture2D image, int screenWidth, int screenHeight, MovingObjectType type) 
        : base(x, y, z, image, screenWidth, screenHeight, type)
    {
        CollisionRectangle = new Rectangle(x, y, image.Width, image.Height);
    }
}