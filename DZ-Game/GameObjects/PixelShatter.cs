using System;
using DZGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects;

public class PixelShatter : BaseMovingObject
{
    private int Radius { get; set; }
    private int Angle { get; set; }
    private int AlienXPos { get; init; }
    private int AlienYPos { get; init; }
    public bool AlienExploding { get; set; }
    
    public PixelShatter(int x, int y, int z, Texture2D image, int screenWidth, int screenHeight, MovingObjectType type, int radius, int angle, int alienXPos, int alienYPos)
        : base(x, y, z, image, screenWidth, screenHeight, type)
    {
        Radius = radius;
        Angle = angle;
        AlienXPos = alienXPos;
        AlienYPos = alienYPos;
        Active = true;
    }

    public override void MoveAuto(GameTime gameTime)
    {
        Radius += 15;
        Angle += 6;
        PositionX = AlienXPos + (int)(Radius * Math.Cos(Angle));
        PositionY = AlienYPos + (int)(Radius * Math.Sin(Angle));

        if (Radius > ScreenWidth)
            Active = false;
    }
}