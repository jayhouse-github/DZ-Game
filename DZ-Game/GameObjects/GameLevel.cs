using System;
using System.Collections.Generic;
using DZGame.GameObjects.Aliens;
using DZGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class GameLevel
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int NoOfAliensAtStart { get; set; }
        public int NoOfAliensLeft { get; set; }
        public int Waves { get; set; }
        public ICollection<IMovingObject> Aliens { get; set; }
        private IList<Texture2D> AlienImages { get; set; }

        public GameLevel(int levelNumber, int screenWidth, int screenHeight, IList<Texture2D> aliens)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Aliens = new List<IMovingObject>();
            AlienImages = aliens;
            InitialiseLevel(levelNumber);
        }

        private void InitialiseLevel(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    for (int i = 50; i <=1000; i = i + 50)
                    {
                        var alien = new Alien1(_screenWidth, _screenHeight, AlienImages[0]);
                        alien.Position_X = i;
                        alien.Position_Y = 300;
                        alien.Position_Z = 1;
                        Aliens.Add(alien);
                    }
                    break;
            }
        }
    }
}
