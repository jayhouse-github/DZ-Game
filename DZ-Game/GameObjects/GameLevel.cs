using System.Collections.Generic;
using DZGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace DZGame.GameObjects
{
    public class GameLevel
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public int NoOfAliensAtStart { get; set; }
        public int NoOfAliens { get; set; } // Active aliens counter
        public int KillThreshold { get; set; }
        public int AlienTypes { get; set; }
        public int AlienDamage { get; set; }
        public int NoOfAliensFiring { get; set; }
        public int Waves { get; set; }
        public int AlienFiringThreshold { get; set; }
        public int AlienBulletDamage { get; set; }
        public int CurrentLevel { get; set; }
        public ICollection<IMovingObject> Aliens { get; set; }
        private IList<Texture2D> AlienImages { get; set; }

        public GameLevel(int levelNumber, int screenWidth, int screenHeight, IList<Texture2D> aliens)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Aliens = new List<IMovingObject>();
            AlienImages = aliens;
            CurrentLevel = levelNumber;
            InitialiseLevel(levelNumber);
        }

        private void InitialiseLevel(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    NoOfAliensAtStart = 20;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = 50;
                    AlienBulletDamage = 5;

                    PopulateAliens(levelNumber);
                    break;
                case 2:
                    NoOfAliensAtStart = 15;
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    KillThreshold = 15;
                    AlienTypes = 2;
                    NoOfAliensFiring = 0;
                    AlienDamage = 0;
                    Waves = 2;

                    PopulateAliens(levelNumber);
                    break;
                 case 3:                                                                                                                                               
                    NoOfAliensAtStart = 20;
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    Waves = 3;                                                                                                                                        
                    PopulateAliens(levelNumber);                                                                                                                      
                    break;                                                                                                                                            
                case 4:                                                                                                                                               
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    Waves = 4;                                                                                                                                        
                    PopulateAliens(levelNumber);                                                                                                                      
                    break;                                                                                                                                            
                case 5:                                                                                                                                               
                    NoOfAliensAtStart = 16;
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    Waves = 4;                                                                                                                                        
                    PopulateAliens(levelNumber);                                                                                                                      
                    break;                                                                                                                                            
                case 6:                                                                                                                                               
                    NoOfAliensAtStart = 25; 
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    Waves = 5;                                                                                                                                        
                    PopulateAliens(levelNumber);                                                                                                                   
                    break;                                                                                                                                        
                case 7:                                                                                                                                             
                    NoOfAliensAtStart = 30; 
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    Waves = 5;                                                                                                                                        
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
                        var alien = new Alien1(x, 300, 1, _screenWidth, _screenHeight, AlienImages[0]);
                        Aliens.Add(alien);
                        x += 50;
                    }
                    break;
                case 2:
                    //TODO - this is only temporary, define actual level 2.
                    x = 200;

                    for (int i = 1; i <= NoOfAliensAtStart; i++)
                    {
                        var alien = new Alien1(x, 40, 1, _screenWidth, _screenHeight, AlienImages[0]);
                        Aliens.Add(alien);
                        x += 50;
                        if (x > 1000) x = 50;
                    }
                    break;
                        case 3:                                                                                                                                               
                case 4:                                                                                                                                               
                case 5:                                                                                                                                               
                case 6:                                                                                                                                               
                case 7:                                                                                                                                               
                    x = 50; // Initial position                                                                                                                   
                    for (int i = 1; i <= NoOfAliensAtStart; i++)                                                                                                      
                    {                                                                                                                                                 
                        var alien = new Alien1(x, 300, 1, _screenWidth,                                                                                               
                        _screenHeight, AlienImages[0]);                                                                                                                                  
                        Aliens.Add(alien);                                                                                                                            
                        x += 50;   
                        if (x > 1000) x = 50;
                    }                                                                                                                                                 
                    break;  
            }
        }

        public void RemoveAlien()
        {
            NoOfAliens--;
        }

        public void ResetAliens()
        {
            Aliens.Clear();
            NoOfAliens = NoOfAliensAtStart;
            PopulateAliens(CurrentLevel);
        }
    }
}
