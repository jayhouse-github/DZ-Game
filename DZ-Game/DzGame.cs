using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DZGame.GameObjects;
using System.Collections.Generic;
using DZGame.Interfaces;
using System;
using System.Linq;

namespace DZ_Game
{
    public class DzGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int screenWidth = 1024;
        private const int screenHeight = 768;
        private const int starCount = 100;

        Texture2D star1;
        Texture2D star2;
        Texture2D star3;
        Texture2D playerImage;
        Texture2D playerBullet;
        Texture2D alien1;
        ICollection<IMovingObject> movingObjects;
        Player player;
        GameLevel gameLevelInfo;
        float starSpeed = 100f;
        int validBullet = 10;
        int gameLevel;
        int currentScore;

        public DzGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            movingObjects = new List<IMovingObject>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameLevel = 1;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();       

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            star1 = Content.Load<Texture2D>("star1");
            star2 = Content.Load<Texture2D>("star2");
            star3 = Content.Load<Texture2D>("star3");
            playerImage = Content.Load<Texture2D>("spaceship1");
            playerBullet = Content.Load<Texture2D>("bullet");
            alien1 = Content.Load<Texture2D>("alien");

            //Populate stars
            for (int i = 0; i < starCount; i++)
            {
                var r = new Random();
                Texture2D selectedStarImage = null;

                switch (r.Next(1, 4))
                {
                    case 1:
                        selectedStarImage = star1;
                        break;
                    case 2:
                        selectedStarImage = star2;
                        break;
                    case 3:
                        selectedStarImage = star3;
                        break;
                }

                movingObjects.Add(new Star(screenWidth, screenHeight, starSpeed, selectedStarImage));
            }

            //Initialise player
            player = new Player(screenWidth / 2, screenHeight - 100, 1, screenWidth, screenHeight, playerImage);
            movingObjects.Add(player);

            //TODO - move to game logic check on update
            var alienImages = new List<Texture2D>();
            alienImages.Add(alien1);
            gameLevelInfo = new GameLevel(gameLevel, screenWidth, screenHeight, alienImages);
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var gState = GamePad.GetState(PlayerIndex.One);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var item in movingObjects)
            {
                item.MoveAuto(gameTime);
            }

            //Move left
            if ((kState.IsKeyDown(Keys.Left) || gState.ThumbSticks.Left.X < 0))
            {
                foreach (var item in movingObjects)
                {
                    item.MoveLeft(gameTime);
                }
            }

            //Move right
            if ((kState.IsKeyDown(Keys.Right) || gState.ThumbSticks.Left.X > 0))
            {
                foreach (var item in movingObjects)
                {
                    item.MoveRight(gameTime);
                }
            }

            //Move forward
            if ((kState.IsKeyDown(Keys.Up) || gState.ThumbSticks.Left.Y > 0))
            {
                foreach (var item in movingObjects)
                {
                    item.MoveUp(gameTime);
                }
            }

            //Move back
            if ((kState.IsKeyDown(Keys.Down) || gState.ThumbSticks.Left.Y < 0))
            {
                foreach (var item in movingObjects)
                {
                    item.MoveDown(gameTime);
                }
            }

            //Fire
            if ((kState.IsKeyDown(Keys.Space) || gState.Buttons.A == ButtonState.Pressed) && validBullet > 9 )
            {
                validBullet = 0;
                movingObjects.Add(new Bullet(player.Position_X + 29, player.Position_Y, playerBullet));
            }

            //New bullet timer
            validBullet++;

            //Clean up
            movingObjects.ToList().RemoveAll(listItem => !listItem.Active);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            //Player, bullets, stars
            foreach (var item in movingObjects)
            {
                _spriteBatch.Draw(item.Image, new Vector2(item.Position_X, item.Position_Y), Color.White);
            }

            //Aliens
            foreach (var alien in gameLevelInfo.Aliens)
            {
                _spriteBatch.Draw(alien.Image, new Vector2(alien.Position_X, alien.Position_Y), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void GetAlienData(int gameLevel)
        {
            //Need to return an object of starting rules and a delegate?? to run for auto move
            //Pass the delegate for auto move to each alien.
            //Get one or two levels working then move on to sound and collision detection
        }
    }
}
