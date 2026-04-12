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
        private SoundEffect _firingSound;
        private SoundEffect _explodeSound;
        private List<IMovingObject> _movingObjects;
        private Player _player;
        //TODO - Don't think I need this
        //GameLevel gameLevelInfo;
        float _starSpeed = 100f;
        int _validBullet = 10;
        int _gameLevel;
        int _currentScore;

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
            _star1 = Content.Load<Texture2D>("star1");
            _star2 = Content.Load<Texture2D>("star2");
            _star3 = Content.Load<Texture2D>("star3");
            _playerImage = Content.Load<Texture2D>("player-1");
            _playerBullet = Content.Load<Texture2D>("bullet");
            //_alien1 = Content.Load<Texture2D>("alien");
            _firingSound = Content.Load<SoundEffect>("laser_sound");
            _pixelShatter = Content.Load<Texture2D>("pixel_shatter");
            _explodeSound = Content.Load<SoundEffect>("explode");
            _alien1 = Content.Load<Texture2D>("alien-1");
            _alien2 = Content.Load<Texture2D>("alien-2");
            _alien3 = Content.Load<Texture2D>("alien-3");
            _alien4 = Content.Load<Texture2D>("alien-4");

            //Initialise title screen - use existing pixel texture
            _titleScreen = new TitleScreen(_pixelShatter, ScreenWidth, ScreenHeight);

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

                //Check for collisions
                CheckForCollisions();

                if (gameLevelInfo.NoOfAliens == 0 && gameLevelInfo.Waves > 0)
                {
                    gameLevelInfo.ResetAliens();
                    _movingObjects.AddRange(gameLevelInfo.Aliens);
                    gameLevelInfo.Waves--;
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

        private void CheckForCollisions()
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
                            alien.Active = false;
                            bullet.Active = false;
                            
                            //Explosion, sound and set off pixel shatter at location
                            CreatePixelCircle(bullet.PositionX, bullet.PositionY);
                            _explodeSound.Play();
                        }
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
            _player = new Player(ScreenWidth / 2, ScreenHeight - 100, 1, ScreenWidth, ScreenHeight, _playerImage);
            _movingObjects.Add(_player);

            //Initialise level
            gameLevelInfo = GetGameLevel(_gameLevel);
            _movingObjects.AddRange(gameLevelInfo.Aliens);
        }
    }
}
