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
        public int NoOfAliens { get; set; }
        public int KillThreshold { get; set; }
        public int AlienTypes { get; set; }
        public int AlienDamage { get; set; }
        public int NoOfAliensFiring { get; set; }
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
                    NoOfAliensAtStart = 20;
                    NoOfAliens = 20;
                    Waves = 3;

                    PopulateAliens(levelNumber);
                    break;
                case 2:
                    NoOfAliensAtStart = 15;
                    NoOfAliens = 40;
                    KillThreshold = 15;
                    AlienTypes = 2;
                    NoOfAliensFiring = 0;
                    AlienDamage = 0;

                    PopulateAliens(levelNumber);
                    break;
            }
        }

        private void PopulateAliens(int levelNumber)
        {
            switch (levelNumber) {

                case 1:
                    var x = 50;

                    for (int i = 1; i <= NoOfAliensAtStart; i++)
                    {
                        var alien = new Alien1(_screenWidth, _screenHeight, AlienImages[0]);
                        alien.Position_X = x;
                        alien.Position_Y = 300;
                        alien.Position_Z = 1;
                        Aliens.Add(alien);
                        x += 50;
                    }
                    break;
                case 2:
                    //TODO - this is only temporary, define actual level 2.
                    x = 200;

                    for (int i = 1; i <= NoOfAliensAtStart; i++)
                    {
                        var alien = new Alien1(_screenWidth, _screenHeight, AlienImages[0]);
                        alien.Position_X = x;
                        alien.Position_Y = 40;
                        alien.Position_Z = 1;
                        Aliens.Add(alien);
                        x += 50;
                        if (x > 1000) x = 50;
                    }
                    break;
            }
        }
    }
}
