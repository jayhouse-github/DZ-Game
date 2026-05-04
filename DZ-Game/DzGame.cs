using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DZGame.GameObjects;
using System.Collections.Generic;
using DZGame.Interfaces;
using System;
using System.Linq;
using DZGame.Enums;
using Microsoft.Xna.Framework.Audio;

namespace DZ_Game
{
    public class DzGame : Game
    {
        //TODO - consider the use of GameScreen to switch between title, level intro and game level modes
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int ScreenWidth = 1024;
        private const int ScreenHeight = 768;
        private const int StarCount = 100;
        private GameLevel gameLevelInfo;
        private GameState _gameState;
        private TitleScreen _titleScreen;

        private Texture2D _star1;
        private Texture2D _star2;
        private Texture2D _star3;
        private Texture2D _playerImage;
        private Texture2D _playerBullet;
        private Texture2D _alien1;
        private Texture2D _alien2;
        private Texture2D _alien3;
        private Texture2D _alien4;
        private Texture2D _pixelShatter;
        private Texture2D _pixelTexture;
        private Texture2D _alienBullet1;
        private SoundEffect _firingSound;
        private SoundEffect _explodeSound;
        private SoundEffect _hitHurtSound;
        private SoundEffect _powerUpSound;
        private SpriteFont _gamefont14;
        private List<IMovingObject> _movingObjects;
        private Player _player;
        //TODO - Don't think I need this
        //GameLevel gameLevelInfo;
        float _starSpeed = 100f;
        int _validBullet = 10;
        int _gameLevel;
        int _currentScore;
        double _lastPlayerCollisionTime = -2.0;
        readonly Random _random = new Random();

        // explosion animation removed - use pixel circle effects only
        private bool _playerDying = false;
        private double _deathTimer = 0;
        private int _deathExplosionCount = 0;
        private int _livesAtDeath = 0;
        // Explosion animation constants removed

        public DzGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _movingObjects = new List<IMovingObject>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _gameLevel = 1;
            _gameState = GameState.TitleScreen;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();       

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { Color.White });
            _star1 = Content.Load<Texture2D>("star1");
            _star2 = Content.Load<Texture2D>("star2");
            _star3 = Content.Load<Texture2D>("star3");
            _playerImage = Content.Load<Texture2D>("player-1");
            _playerBullet = Content.Load<Texture2D>("bullet");
            _alien1 = Content.Load<Texture2D>("alien");
            //_firingSound = Content.Load<SoundEffect>("laser_sound");
            _firingSound = Content.Load<SoundEffect>("laserShoot");
            _pixelShatter = Content.Load<Texture2D>("pixel_shatter");
            _explodeSound = Content.Load<SoundEffect>("explosion");
            _hitHurtSound = Content.Load<SoundEffect>("hitHurt");
            _powerUpSound = Content.Load<SoundEffect>("powerUp");
            _alien1 = Content.Load<Texture2D>("alien-1");
            _alien2 = Content.Load<Texture2D>("alien-2");
            _alien3 = Content.Load<Texture2D>("alien-3");
            _alien4 = Content.Load<Texture2D>("alien-4");
            _alienBullet1 = Content.Load<Texture2D>("alienBullet");
            _gamefont14 = Content.Load<SpriteFont>("GameFont1-14");
            // player explosion spritesheet not loaded - using pixel circle effects only

            //Initialise title screen - use game font
            _titleScreen = new TitleScreen(_gamefont14, ScreenWidth, ScreenHeight);

            //Populate stars
            for (int i = 0; i < StarCount; i++)
            {
                var r = new Random();
                var x = r.Next(ScreenWidth);
                var y = r.Next(ScreenHeight);
                var z = r.Next(1, 4);
                Texture2D selectedStarImage = null;

                switch (r.Next(1, 4))
                {
                    case 1:
                        selectedStarImage = _star1;
                        break;
                    case 2:
                        selectedStarImage = _star2;
                        break;
                    case 3:
                        selectedStarImage = _star3;
                        break;
                }

                _movingObjects.Add(new Star(x, y, z, _starSpeed, selectedStarImage, ScreenWidth, ScreenHeight));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var gState = GamePad.GetState(PlayerIndex.One);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_gameState == GameState.TitleScreen)
            {
                // Only animate stars on title screen
                foreach (var item in _movingObjects)
                {
                    item.MoveAuto(gameTime);
                }

                // Check for fire button to start game
                if (kState.IsKeyDown(Keys.Space) || gState.Buttons.A == ButtonState.Pressed)
                {
                    _gameState = GameState.Playing;
                    StartGame();
                }
            }
            else if (_gameState == GameState.Playing)
            {
                foreach (var item in _movingObjects)
                {
                    item.MoveAuto(gameTime);
                }

                //Move left
                if ((kState.IsKeyDown(Keys.Left) || gState.ThumbSticks.Left.X < 0))
                {
                    foreach (var item in _movingObjects)
                    {
                        item.MoveLeft(gameTime);
                    }
                }

                //Move right
                if ((kState.IsKeyDown(Keys.Right) || gState.ThumbSticks.Left.X > 0))
                {
                    foreach (var item in _movingObjects)
                    {
                        item.MoveRight(gameTime);
                    }
                }

                //Move forward
                if ((kState.IsKeyDown(Keys.Up) || gState.ThumbSticks.Left.Y > 0))
                {
                    foreach (var item in _movingObjects)
                    {
                        item.MoveUp(gameTime);
                    }
                }

                //Move back
                if ((kState.IsKeyDown(Keys.Down) || gState.ThumbSticks.Left.Y < 0))
                {
                    foreach (var item in _movingObjects)
                    {
                        item.MoveDown(gameTime);
                    }
                }

                //Fire
                if ((kState.IsKeyDown(Keys.Space) || gState.Buttons.A == ButtonState.Pressed) && _validBullet > 9 )
                {
                    _validBullet = 0;
                    _movingObjects.Add(new Bullet(_player.PositionX + 29, _player.PositionY, 1, _playerBullet));
                    _firingSound.Play();
                }

                //New bullet timer
                _validBullet++;

                //Clean up
                _movingObjects.RemoveAll(listItem => !listItem.Active);

                //Alien firing
                if (_random.Next(1, gameLevelInfo.AlienFiringThreshold + 1) == 2)
                {
                    var activeAliens = _movingObjects.Where(o => o.MoveType == MovingObjectType.Alien && o.Active).ToList();
                    if (activeAliens.Any())
                    {
                        var firingAlien = (Alien)activeAliens[_random.Next(activeAliens.Count)];
                        _movingObjects.Add(new AlienBullet(firingAlien.PositionX, firingAlien.PositionY, 1, ScreenWidth, ScreenHeight, _alienBullet1, firingAlien.BulletsDestroyable, gameLevelInfo.AlienBulletDamage));
                    }
                }

                //Check for collisions
                CheckForCollisions(gameTime);

                if (_playerDying)
                {
                    _deathTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (_deathTimer >= 0.5 && _deathExplosionCount < 3)
                    {
                        CreatePixelCircle(_player.PositionX, _player.PositionY);
                        _explodeSound.Play();
                        _deathExplosionCount++;
                        _deathTimer = 0;
                    }
                    if (_deathExplosionCount >= 3 && _deathTimer >= 0.5 && _livesAtDeath > 0)
                    {
                        _playerDying = false;
                        ResetLevel();
                        _player = new Player(ScreenWidth / 2, ScreenHeight - 110, 1, ScreenWidth, ScreenHeight, _playerImage);
                        _player.Lives = _livesAtDeath - 1;
                        _player.ShieldStrength = 10;
                        _movingObjects.Add(_player);
                    }
                    else if (_deathExplosionCount >= 3 && _deathTimer >= 0.5 && _livesAtDeath <= 0)
                    {
                        _playerDying = false;
                    }

                    // no sprite animation to update for player death; only pixel circles
                }

                if (gameLevelInfo.NoOfAliens == 0 && gameLevelInfo.Waves > 0)
                {
                    gameLevelInfo.Waves--;
                    if (gameLevelInfo.Waves > 0)
                    {
                        gameLevelInfo.ResetAliens();
                        _movingObjects.AddRange(gameLevelInfo.Aliens);
                        _powerUpSound.Play();
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            //Draw stars (always visible)
            foreach (var item in _movingObjects)
            {
                _spriteBatch.Draw(item.Image, new Vector2(item.PositionX, item.PositionY), Color.White);
            }

            if (_gameState == GameState.TitleScreen)
            {
                _titleScreen.Draw(_spriteBatch);
            }
            else if (_gameState == GameState.Playing)
            {
                _spriteBatch.DrawString(_gamefont14, $"SCORE {_currentScore}", new Vector2(10, 10), Color.Red);
                var livesText = $"LIVES {_player.Lives}";
                var livesTextSize = _gamefont14.MeasureString(livesText);
                var livesX = ScreenWidth - livesTextSize.X - 10;
                _spriteBatch.DrawString(_gamefont14, livesText, new Vector2(livesX, 10), Color.Red);
                if (_player.ShieldStrength > 0)
                {
                    var shieldBarWidth = (int)(livesTextSize.X * _player.ShieldStrength / 10f);
                    _spriteBatch.Draw(_pixelTexture, new Microsoft.Xna.Framework.Rectangle((int)livesX, 10 + (int)livesTextSize.Y + 2, shieldBarWidth, 4), Color.Red);
                }
                var wavesText = $"WAVES {gameLevelInfo.Waves}";
                var wavesTextSize = _gamefont14.MeasureString(wavesText);
                _spriteBatch.DrawString(_gamefont14, wavesText, new Vector2((ScreenWidth - wavesTextSize.X) / 2, 10), Color.Red);

                // player death has no sprite animation to draw; pixel circles are visible via _movingObjects
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private GameLevel GetGameLevel(int gameLevel)
        {
            //This populates the alien images for the level
            var alienImages = new List<Texture2D>();

            switch (gameLevel)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    alienImages.Add(_alien3);
                    break;
            }

            return new GameLevel(gameLevel, ScreenWidth, ScreenHeight, alienImages);
        }

        private void CheckForCollisions(GameTime gameTime)
        {
            var liveBullets = _movingObjects.Where(
                o
                    => o.MoveType == MovingObjectType.PlayerBullet
                       && o.Active).ToList();
            var alienObjects = _movingObjects.Where(
                o
                    => o.MoveType == MovingObjectType.Alien
                       && o.Active).ToList();

            if (liveBullets.Any())
            {
                foreach (var bullet in liveBullets.Cast<Bullet>())
                {
                    foreach (var alien in alienObjects.Cast<Alien>())
                    {
                        if (bullet.CollisionRectangle.IntersectsWith(alien.CollisionRectangle))
                        {
                            //Mark alien and bullet as inactive
                            gameLevelInfo.RemoveAlien();
                            _currentScore += alien.ScoreValue;
                            alien.Active = false;
                            bullet.Active = false;

                            //Explosion, sound and set off pixel shatter at location
                            CreatePixelCircle(bullet.PositionX, bullet.PositionY);
                            _explodeSound.Play();
                        }
                    }
                }
            }

            // Check if any alien bullet has hit the player
            var alienBullets = _movingObjects.Where(
                o => o.MoveType == MovingObjectType.AlienBullet && o.Active).ToList();
            foreach (var ab in alienBullets.Cast<AlienBullet>())
            {
                if (ab.CollisionRectangle.IntersectsWith(_player.CollisionRectangle))
                {
                    ab.Active = false;
                    _player.ShieldStrength -= ab.Damage;

                    if (_player.ShieldStrength <= 0 && !_playerDying)
                    {
                        StartPlayerDeath();
                    }
                    else
                    {
                        _hitHurtSound.Play();
                    }
                }
            }

            // Check if player bullets destroy alien bullets (only destroyable ones)
            var destroyableAlienBullets = _movingObjects.Where(
                o => o.MoveType == MovingObjectType.AlienBullet && o.Active).Cast<AlienBullet>().ToList();

            foreach (var ab in destroyableAlienBullets.Where(b => b.Destroyable))
            {
                foreach (var bullet in liveBullets.Cast<Bullet>())
                {
                    if (ab.CollisionRectangle.IntersectsWith(bullet.CollisionRectangle))
                    {
                        ab.Active = false;
                        bullet.Active = false;

                        // TODO: Add animation here for alien bullet destruction
                    }
                }
            }

            // Check if any alien has collided with the player (2-second cooldown between hits)
            var totalSeconds = gameTime.TotalGameTime.TotalSeconds;
            if (totalSeconds - _lastPlayerCollisionTime >= 1.0)
            {
                foreach (var alien in alienObjects.Cast<Alien>())
                {
                    if (alien.CollisionRectangle.IntersectsWith(_player.CollisionRectangle))
                    {
                        _lastPlayerCollisionTime = totalSeconds;
                        _player.ShieldStrength -= alien.Strength;

                        if (_player.ShieldStrength <= 0)
                        {
                            StartPlayerDeath();
                        }
                        else
                        {
                            _hitHurtSound.Play();
                        }

                        break;
                    }
                }
            }
        }

        private void CreatePixelCircle(int alienX, int alienY)
        {
            var angle = 1;
            var x = 0;
            var y = 0;

            for (int i = 1; i <= 36; i++)
            {
                x = alienX + (int)(10 * Math.Cos(angle));
                y = alienY + (int)(10 * Math.Sin(angle));

                _movingObjects.Add(new PixelShatter(x, y, 1, _pixelShatter, ScreenWidth, ScreenHeight, MovingObjectType.PixelShatter, 100, angle, alienX, alienY));
                angle += 10;
            }
        }

        private void StartGame()
        {
            //Initialise player
            _player = new Player(ScreenWidth / 2, ScreenHeight - 110, 1, ScreenWidth, ScreenHeight, _playerImage);
            _player.Lives = 3;
            _movingObjects.Add(_player);

            //Initialise level
            gameLevelInfo = GetGameLevel(_gameLevel);
            _movingObjects.AddRange(gameLevelInfo.Aliens);

            _powerUpSound.Play();
        }

        private void ResetLevel()
        {
            _movingObjects.RemoveAll(o => o.MoveType == MovingObjectType.Alien || o.MoveType == MovingObjectType.AlienBullet || o.MoveType == MovingObjectType.PlayerBullet);
            gameLevelInfo = GetGameLevel(_gameLevel);
            _movingObjects.AddRange(gameLevelInfo.Aliens);

            _powerUpSound.Play();
        }

        private void StartPlayerDeath()
        {
            _playerDying = true;
            _deathTimer = 0;
            _deathExplosionCount = 0;
            _livesAtDeath = _player.Lives;
            CreatePixelCircle(_player.PositionX, _player.PositionY);
            _explodeSound.Play();
            _deathExplosionCount++;
            _player.Active = false;

            // no sprite animation created here - only the pixel circles are used for the death effect

            _movingObjects.RemoveAll(o => o.MoveType == MovingObjectType.Alien || o.MoveType == MovingObjectType.AlienBullet || o.MoveType == MovingObjectType.PlayerBullet);
        }
    }
}
