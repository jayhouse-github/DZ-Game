using System;
using System.Collections.Generic;
using DZGame.Interfaces;
using Microsoft.Xna.Framework;
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
        public int ShieldStrengthPerAlien { get; set; }

        public int AlienScoreValue { get; set; }
        public int AlienStrength { get; set; }
        public bool AlienBulletsDestroyable { get; set; }

        public int AlienFirePowerUpThreshold { get; set; }
        public int ShieldPowerUpValue { get; set; }
        public ICollection<IMovingObject> Aliens { get; set; }
        private IList<Texture2D> AlienImages { get; set; }
        private Func<int> _getPlayerX;
        private Func<int> _getPlayerY;

        public GameLevel(int levelNumber, int screenWidth, int screenHeight, IList<Texture2D> aliens,
            Func<int> getPlayerX = null, Func<int> getPlayerY = null)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Aliens = new List<IMovingObject>();
            AlienImages = aliens;
            CurrentLevel = levelNumber;
            _getPlayerX = getPlayerX;
            _getPlayerY = getPlayerY;
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
                    AlienScoreValue = 10;
                    AlienStrength = 1;
                    ShieldStrengthPerAlien = 1;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = 100;
                    ShieldPowerUpValue = 2;

                    PopulateAliens(levelNumber);
                    break;
                case 2:
                    NoOfAliensAtStart = 20;
                    NoOfAliens = NoOfAliensAtStart; // Initialize with the starting number of aliens
                    KillThreshold = 15;
                    AlienTypes = 2;
                    NoOfAliensFiring = 0;
                    AlienDamage = 0;
                    Waves = 2;
                    AlienFiringThreshold = -1;
                    AlienBulletDamage = 5;
                    AlienScoreValue = 10;
                    AlienStrength = 1;
                    ShieldStrengthPerAlien = 1;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 2;

                    PopulateAliens(levelNumber);
                    break;
                case 3:
                    NoOfAliensAtStart = 20;
                    NoOfAliens = NoOfAliensAtStart;
                    KillThreshold = 15;
                    AlienTypes = 2;
                    NoOfAliensFiring = 0;
                    AlienDamage = 0;
                    Waves = 2;
                    AlienFiringThreshold = 50;
                    AlienBulletDamage = 5;
                    AlienScoreValue = 10;
                    AlienStrength = 3;
                    ShieldStrengthPerAlien = 1;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 2;

                    PopulateAliens(levelNumber);
                    break;
                case 4:
                    NoOfAliensAtStart = 15;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = 40;
                    AlienBulletDamage = 4;
                    AlienScoreValue = 20;
                    AlienStrength = 2;
                    ShieldStrengthPerAlien = 2;
                    AlienBulletsDestroyable = false;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;

                    PopulateAliens(levelNumber);
                    break;                                                                                                                                            
                case 5:
                    NoOfAliensAtStart = 20;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 4;
                    AlienFiringThreshold = -1;
                    AlienBulletDamage = 0;
                    AlienScoreValue = 15;
                    AlienStrength = 2;
                    ShieldStrengthPerAlien = 1;
                    AlienBulletsDestroyable = false;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;

                    PopulateAliens(levelNumber);
                    break;                                                                                                                                            
                case 6:
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;
                    AlienBulletDamage = 0;
                    AlienScoreValue = 15;
                    AlienStrength = 3;
                    ShieldStrengthPerAlien = 1;
                    AlienBulletsDestroyable = false;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;

                    PopulateAliens(levelNumber);
                    break;                                                                                                                                        
                case 7:
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;
                    AlienBulletDamage = 3;
                    AlienScoreValue = 30;
                    AlienStrength = 3;
                    ShieldStrengthPerAlien = 3;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;
                    PopulateAliens(levelNumber);
                    break;
                case 8:
                    // 6 Alien7 sine-wave sweepers + 6 Alien8 diagonal bouncers (tough, 5 shields each)
                    // + 6 Alien9 dive bombers (1-hit kill, fires undestroyable bullets while diving)
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;     // divers handle their own firing via WantsToFire
                    AlienBulletDamage = 4;
                    AlienScoreValue = 20;          // default; divers override to 30 in PopulateAliens
                    AlienStrength = 2;
                    ShieldStrengthPerAlien = 5;    // roamers; divers override to 1 in PopulateAliens
                    AlienBulletsDestroyable = false;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;
                    PopulateAliens(levelNumber);
                    break;
                case 9:
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;
                    AlienBulletDamage = 3;
                    AlienScoreValue = 20;
                    AlienStrength = 3;
                    ShieldStrengthPerAlien = 3;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;
                    PopulateAliens(levelNumber);
                    break;
                case 10:
                    // Spirograph: two counter-rotating rings around a moving hub; aliens sling off
                    // at a tangent, fire once, then return to their slot
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;     // slung aliens handle their own firing via WantsToFire
                    AlienBulletDamage = 3;
                    AlienScoreValue = 25;
                    AlienStrength = 2;
                    ShieldStrengthPerAlien = 2;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;
                    PopulateAliens(levelNumber);
                    break;
                case 11:
                    // Circuit racers: two chains race nose-to-tail round hard-cornered tracks
                    // (square-wave battlements + bow-tie), firing as they turn into gun-port corners
                    NoOfAliensAtStart = 18;
                    NoOfAliens = NoOfAliensAtStart;
                    Waves = 3;
                    AlienFiringThreshold = -1;     // racers handle their own firing via WantsToFire
                    AlienBulletDamage = 3;
                    AlienScoreValue = 25;
                    AlienStrength = 3;             // ramming hurts - the strafing lane runs at ship height
                    ShieldStrengthPerAlien = 2;
                    AlienBulletsDestroyable = true;
                    AlienFirePowerUpThreshold = -1;
                    ShieldPowerUpValue = 0;
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
                        var alien = new Alien1(x, 300, 1, _screenWidth, _screenHeight, AlienImages[0], this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable, this.ShieldStrengthPerAlien);
                        Aliens.Add(alien);
                        x += 50;
                    }
                    break;
                case 2:
                    var rand = new Random();
                    for (int i = 1; i <= NoOfAliensAtStart; i++)
                    {
                        var alien = new Alien2(rand.Next(35, _screenWidth - 35), rand.Next(40, _screenHeight - 150), 1, _screenWidth, _screenHeight, AlienImages[0], this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable, this.ShieldStrengthPerAlien);
                        Aliens.Add(alien);
                    }
                    break;
                case 3:
                    int centerX = _screenWidth / 2;
                    int centerY = _screenHeight / 3;
                    for (int i = 0; i < NoOfAliensAtStart; i++)
                    {
                        double angle = 2 * Math.PI * i / NoOfAliensAtStart;
                        var alien = new Alien3(centerX, centerY, 1, _screenWidth, _screenHeight, AlienImages[0], this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable, this.ShieldStrengthPerAlien, angle);
                        Aliens.Add(alien);
                    }
                    break;
                case 5:
                    var rand5 = new Random(77);
                    for (int i = 0; i < NoOfAliensAtStart; i++)
                    {
                        // Spread centres across the upper portion of the screen
                        int spawnX5 = 80 + rand5.Next(_screenWidth - 160);
                        int spawnY5 = 60 + rand5.Next((int)(_screenHeight * 0.45));
                        // Phase delta drives the curve shape - spread them across the full range so
                        // every alien traces a visually distinct Lissajous path
                        double phaseDelta = (2.0 * Math.PI * i) / NoOfAliensAtStart + rand5.NextDouble() * 0.8;
                        double amplX = 70 + rand5.NextDouble() * 80;   // 70..150 px
                        double amplY = 40 + rand5.NextDouble() * 50;   // 40..90 px
                        double speed = 2.8 + rand5.NextDouble() * 1.4; // 2.8..4.2 rad/s — fast but not crazy
                        double driftAngle = rand5.NextDouble() * Math.PI * 2;
                        double driftSpeed = 18 + rand5.NextDouble() * 22; // 18..40 px/s drift
                        var alien5 = new Alien5(spawnX5, spawnY5, 1, _screenWidth, _screenHeight, AlienImages[0],
                            this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable,
                            this.ShieldStrengthPerAlien, phaseDelta, amplX, amplY, speed, driftAngle, driftSpeed);
                        Aliens.Add(alien5);
                    }
                    break;
                case 6:
                    var rand6 = new Random(99);
                    for (int i = 0; i < NoOfAliensAtStart; i++)
                    {
                        // Spread orbit centres across upper 30% of screen
                        int cx6 = 80 + rand6.Next(_screenWidth - 160);
                        int cy6 = 55 + rand6.Next((int)(_screenHeight * 0.25));
                        double orbitRX = 50 + rand6.NextDouble() * 60;   // 50–110 px
                        double orbitRY = 25 + rand6.NextDouble() * 35;   // 25–60 px
                        // Alternate CW/CCW and vary speed for each alien
                        double orbitSpeed = (3.0 + rand6.NextDouble() * 1.5) * (i % 2 == 0 ? 1 : -1);
                        double initAngle = rand6.NextDouble() * Math.PI * 2;
                        // Stagger initial roam durations so swoops don't all fire at once
                        double roamDuration = 0.5 + rand6.NextDouble() * 3.5;
                        // Alternate between the two alien images for visual variety
                        var img6 = AlienImages[i % AlienImages.Count];
                        var alien6 = new Alien6(cx6, cy6, 1, _screenWidth, _screenHeight, img6,
                            this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable,
                            this.ShieldStrengthPerAlien, orbitRX, orbitRY, orbitSpeed,
                            initAngle, roamDuration, _getPlayerX, i * 17 + 99);
                        Aliens.Add(alien6);
                    }
                    break;
                case 7:
                {
                    var rand7 = new Random(707);
                    int halfCount = NoOfAliensAtStart / 2;

                    for (int i = 0; i < halfCount; i++)
                    {
                        int spawnX = 80 + i * ((_screenWidth - 160) / Math.Max(1, halfCount - 1));
                        int spawnY = 75 + rand7.Next((int)(_screenHeight * 0.18));
                        double orbitRadiusX = 45 + rand7.NextDouble() * 35;
                        double orbitRadiusY = 25 + rand7.NextDouble() * 25;
                        double orbitSpeed = (2.6 + rand7.NextDouble() * 1.2) * (i % 2 == 0 ? 1 : -1);
                        double angle = rand7.NextDouble() * Math.PI * 2;
                        double roamDuration = 1.2 + rand7.NextDouble() * 1.2;

                        Aliens.Add(new Alien10(spawnX, spawnY, 1, _screenWidth, _screenHeight,
                            AlienImages[0], AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                            3, orbitRadiusX, orbitRadiusY, orbitSpeed, angle, roamDuration,
                            _getPlayerX, i * 37 + 701));
                    }

                    for (int i = 0; i < halfCount; i++)
                    {
                        int spawnX = 80 + i * ((_screenWidth - 160) / Math.Max(1, halfCount - 1));
                        int spawnY = 110 + rand7.Next((int)(_screenHeight * 0.2));
                        double horizontalSpeed = 120 + rand7.NextDouble() * 50;
                        double amplitude = 35 + rand7.NextDouble() * 25;
                        double frequency = 1.8 + rand7.NextDouble() * 0.8;
                        double phase = (2.0 * Math.PI * i) / halfCount;
                        double fireDelay = rand7.NextDouble() * 2.2;

                        Aliens.Add(new Alien11(spawnX, spawnY, 1, _screenWidth, _screenHeight,
                            AlienImages[1], AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                            3, horizontalSpeed, amplitude, frequency, 2.6, phase, fireDelay, _getPlayerX));
                    }
                    break;
                }
                case 4:
                    var rand4 = new Random(42);
                    // Spread aliens across the top half in loose clusters
                    int cols = 5;
                    int rows = 3;
                    int cellW = (_screenWidth - 120) / cols;
                    int cellH = (_screenHeight / 2 - 80) / rows;
                    int a4idx = 0;
                    for (int row = 0; row < rows && a4idx < NoOfAliensAtStart; row++)
                    {
                        for (int col = 0; col < cols && a4idx < NoOfAliensAtStart; col++)
                        {
                            int spawnX = 60 + col * cellW + rand4.Next(cellW / 4);
                            int spawnY = 60 + row * cellH + rand4.Next(cellH / 4);
                            double phase = rand4.NextDouble() * Math.PI * 2;
                            double scaleX = 80 + rand4.NextDouble() * 80;  // 80..160 px
                            double scaleY = 35 + rand4.NextDouble() * 40;  // 35..75 px
                            double driftSpeed = 6 + rand4.NextDouble() * 10; // 6..16 px/s
                            var alien4 = new Alien4(spawnX, spawnY, 1, _screenWidth, _screenHeight, AlienImages[0],
                                this.AlienStrength, this.AlienScoreValue, this.AlienBulletsDestroyable,
                                this.ShieldStrengthPerAlien, phase, scaleX, scaleY, driftSpeed);
                            Aliens.Add(alien4);
                            a4idx++;
                        }
                    }
                    break;
                case 8:
                {
                    var rnd8 = new Random(13);

                    // --- Alien7: sine-wave sweepers (alien-2.png = AlienImages[0]) ---
                    double[] a7BaseY = { 70, 100, 130, 80, 110, 90 };
                    for (int i = 0; i < 6; i++)
                    {
                        int spawnX = 80 + i * ((_screenWidth - 160) / 5);
                        double velX = (i % 2 == 0 ? 1 : -1) * (150 + rnd8.NextDouble() * 40);
                        double amplitude = 45 + rnd8.NextDouble() * 30;   // 45–75 px
                        double frequency = 1.6 + rnd8.NextDouble() * 0.8; // 1.6–2.4 rad/s
                        double phase = (2.0 * Math.PI * i) / 6;
                        Aliens.Add(new Alien7(spawnX, (int)a7BaseY[i], 1, _screenWidth, _screenHeight,
                            AlienImages[0], AlienStrength, AlienScoreValue, false, 3,
                            velX, amplitude, frequency, phase));
                    }

                    // --- Alien8: fast erratic free-movers (alien-4.png = AlienImages[1]) ---
                    double[] angles = { 35, 55, 130, 150, 210, 310 };
                    for (int i = 0; i < 6; i++)
                    {
                        int spawnX = 60 + rnd8.Next(_screenWidth - 120);
                        int spawnY = 50 + rnd8.Next((int)(_screenHeight * 0.35));
                        double speed = 300 + rnd8.NextDouble() * 150;  // 300–450 px/s
                        double rad = angles[i] * Math.PI / 180.0;
                        double velX8 = speed * Math.Cos(rad);
                        double velY8 = speed * Math.Sin(rad);
                        Aliens.Add(new Alien8(spawnX, spawnY, 1, _screenWidth, _screenHeight,
                            AlienImages[1], AlienStrength, AlienScoreValue, false, 3,
                            velX8, velY8, i * 17 + 53));
                    }

                    // --- Alien9: dive bombers (alien-1.png = AlienImages[2]) ---
                    for (int i = 0; i < 6; i++)
                    {
                        int spawnX = 80 + i * ((_screenWidth - 160) / 5);
                        double roamVX = (i % 2 == 0 ? 1 : -1) * (45 + rnd8.NextDouble() * 20);
                        double roamDuration = 1.0 + i * 0.6 + rnd8.NextDouble() * 0.5;
                        Aliens.Add(new Alien9(spawnX, 80, 1, _screenWidth, _screenHeight,
                            AlienImages[2], AlienStrength, 30, false, 1,
                            roamVX, roamDuration, _getPlayerX, i * 31 + 7));
                    }
                    break;
                }
                case 9:
                {
                    const int formationRows = 3;
                    const int formationColumns = 6;

                    for (int row = 0; row < formationRows; row++)
                    {
                        for (int column = 0; column < formationColumns; column++)
                        {
                            int firstShooterColumn = (row * 2 + 1) % formationColumns;
                            int secondShooterColumn = (firstShooterColumn + formationColumns / 2) % formationColumns;
                            bool isVolleyShooter = column == firstShooterColumn || column == secondShooterColumn;
                            var image = row % 2 == 0 ? AlienImages[0] : AlienImages[1];
                            int initialX = _screenWidth / 2 + (column - (formationColumns - 1) / 2) * 78;
                            int initialY = 75 + row * 52;
                            double fireDelay = 0.4 + row * 0.5
                                + (column == secondShooterColumn ? 0.65 : 0);

                            Aliens.Add(new Alien12(initialX, initialY, 1, _screenWidth, _screenHeight,
                                image, AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                                ShieldStrengthPerAlien, row, column, formationRows, formationColumns,
                                isVolleyShooter, 3.4, fireDelay));
                        }
                    }
                    break;
                }
                case 10:
                {
                    var rand10 = new Random(1010);
                    const int outerCount = 12;
                    const int innerCount = 6;

                    // Outer ring spins clockwise, inner ring spins anticlockwise
                    for (int i = 0; i < outerCount + innerCount; i++)
                    {
                        bool isOuter = i < outerCount;
                        int ringIndex = isOuter ? i : i - outerCount;
                        int ringCount = isOuter ? outerCount : innerCount;
                        double ringRadius = isOuter ? 150 : 80;
                        double ringSpeed = isOuter ? 0.9 : -1.6;
                        double slotAngle = 2.0 * Math.PI * ringIndex / ringCount;
                        double slingDelay = 1.5 + rand10.NextDouble() * 6.5;

                        Aliens.Add(new Alien13(_screenWidth / 2, 230, 1, _screenWidth, _screenHeight,
                            AlienImages[0], AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                            ShieldStrengthPerAlien, ringRadius, ringSpeed, slotAngle, slingDelay,
                            i * 29 + 1001));
                    }
                    break;
                }
                case 11:
                {
                    // Track A: square-wave battlements whose teeth plunge into the player's zone, then a
                    // low strafing run back across the bottom, skimming just above the ship's lowest
                    // position so the player can duck under it (clockwise)
                    const int trackALeft = 60;
                    const int toothWidth = 120;
                    const float battlementTop = 60;
                    const float battlementBottom = 470;
                    const float returnLane = 570;
                    var trackA = new List<Vector2> { new Vector2(trackALeft, battlementTop) };
                    var gunPortsA = new List<int>();
                    float toothX = trackALeft;
                    for (int tooth = 0; tooth < 3; tooth++)
                    {
                        toothX += toothWidth;
                        trackA.Add(new Vector2(toothX, battlementTop));
                        trackA.Add(new Vector2(toothX, battlementBottom));
                        gunPortsA.Add(trackA.Count - 1);   // bottom of each tooth
                        toothX += toothWidth;
                        trackA.Add(new Vector2(toothX, battlementBottom));
                        trackA.Add(new Vector2(toothX, battlementTop));
                    }
                    toothX += toothWidth;
                    trackA.Add(new Vector2(toothX, battlementTop));
                    trackA.Add(new Vector2(toothX, returnLane));
                    trackA.Add(new Vector2(trackALeft, returnLane));

                    // Track B: bow-tie whose diagonals cross (top runs left, bottom runs left); the whole
                    // shape slides sideways to sit over the player
                    const int bowTieHalfWidth = 230;
                    var trackB = new[]
                    {
                        new Vector2(_screenWidth / 2 + bowTieHalfWidth, 130),
                        new Vector2(_screenWidth / 2 - bowTieHalfWidth, 130),
                        new Vector2(_screenWidth / 2 + bowTieHalfWidth, 400),
                        new Vector2(_screenWidth / 2 - bowTieHalfWidth, 400)
                    };
                    var gunPortsB = new[] { 1, 2 };    // diagonal dive and bottom edge
                    double maxFollowOffset = _screenWidth / 2 - bowTieHalfWidth - 60;

                    var waypointsA = trackA.ToArray();
                    var portsA = gunPortsA.ToArray();
                    double lengthA = TrackLength(waypointsA);
                    double lengthB = TrackLength(trackB);
                    const int countA = 10;
                    const int countB = 8;

                    for (int i = 0; i < countA; i++)
                    {
                        Aliens.Add(new Alien14(0, 0, 1, _screenWidth, _screenHeight,
                            AlienImages[0], AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                            ShieldStrengthPerAlien, waypointsA, 560, lengthA * i / countA, portsA, 2.0));
                    }

                    for (int i = 0; i < countB; i++)
                    {
                        Aliens.Add(new Alien14(0, 0, 1, _screenWidth, _screenHeight,
                            AlienImages[1], AlienStrength, AlienScoreValue, AlienBulletsDestroyable,
                            ShieldStrengthPerAlien, trackB, 620, lengthB * i / countB, gunPortsB, 1.4,
                            _getPlayerX, 160, maxFollowOffset));
                    }
                    break;
                }
            }
        }

        private static double TrackLength(Vector2[] waypoints)
        {
            double length = 0;
            for (int i = 0; i < waypoints.Length; i++)
                length += Vector2.Distance(waypoints[i], waypoints[(i + 1) % waypoints.Length]);
            return length;
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
