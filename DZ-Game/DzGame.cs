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

        private Texture2D _star1;
        private Texture2D _star2;
        private Texture2D _star3;
        private Texture2D _playerImage;
        private Texture2D _playerBullet;
        private Texture2D _alien1;
        private SoundEffect _firingSound;
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
            _gameLevel = 2;
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
            _playerImage = Content.Load<Texture2D>("spaceship1");
            _playerBullet = Content.Load<Texture2D>("bullet");
            _alien1 = Content.Load<Texture2D>("alien");
            _firingSound = Content.Load<SoundEffect>("laser_sound");

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

            //Initialise player
            _player = new Player(ScreenWidth / 2, ScreenHeight - 100, 1, ScreenWidth, ScreenHeight, _playerImage);
            _movingObjects.Add(_player);

            //Initialise level
            var gameLevelInfo = GetGameLevel(_gameLevel);
            _movingObjects.AddRange(gameLevelInfo.Aliens);
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var gState = GamePad.GetState(PlayerIndex.One);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            //Player, bullets, stars
            foreach (var item in _movingObjects)
            {
                _spriteBatch.Draw(item.Image, new Vector2(item.PositionX, item.PositionY), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private GameLevel GetGameLevel(int gameLevel)
        {
            var alienImages = new List<Texture2D>();

            switch (gameLevel)
            {
                case 1:
                case 2:
                    alienImages.Add(_alien1);
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
                            alien.Active = false;
                            bullet.Active = false;
                            
                            //Explosion, sound and set off pixel shatter at location
                        }
                    }
                }
            }
        }
    }
}
