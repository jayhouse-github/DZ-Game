using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DZGame.GameObjects;
using System.Collections;
using System.Collections.Generic;
using DZGame.GameInterfaces;
using System;

namespace DZ_Game
{
    public class Game1 : Game
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
        ICollection<Star> starsCollection;
        ICollection<Bullet> playerBullets;
        ICollection<IMovingObject> movingObjects;
        Player player;
        float starSpeed = 100f;
        int validBullet;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            starsCollection = new List<Star>();
            movingObjects = new List<IMovingObject>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            //Populate stars
            for (int i = 0; i < starCount; i++)
            {
                var r = new Random();
                Texture2D selectedStarImage = null;

                switch(r.Next(1, 4)){
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

            player = new Player(screenWidth / 2, screenHeight - 100, 1, screenWidth, screenHeight, playerImage);
            validBullet = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            star1 = Content.Load<Texture2D>("star1");
            star2 = Content.Load<Texture2D>("star2");
            star3 = Content.Load<Texture2D>("star3");
            playerImage = Content.Load<Texture2D>("spaceship1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kState = Keyboard.GetState();
            var gState = GamePad.GetState(PlayerIndex.One);

            // TODO: Add your update logic here
            foreach (var star in movingObjects)
            {
                star.MoveAuto(gameTime);
            }

            //Move left
            if ((kState.IsKeyDown(Keys.Left) || gState.ThumbSticks.Left.X < 0))
            {
                foreach (var star in starsCollection)
                {
                    star.MoveLeft(gameTime);
                }

                player.MoveLeft(gameTime);
            }

            //Move right
            if ((kState.IsKeyDown(Keys.Right) || gState.ThumbSticks.Left.X > 0))
            {
                foreach (var star in starsCollection)
                {
                    star.MoveRight(gameTime);
                }

                player.MoveRight(gameTime);
            }

            //Move forward
            if ((kState.IsKeyDown(Keys.Up) || gState.ThumbSticks.Left.Y > 0))
            {
                foreach (var star in starsCollection)
                {
                    star.MoveUp(gameTime);
                }

                player.MoveUp(gameTime);
            }

            //Move back
            if ((kState.IsKeyDown(Keys.Down) || gState.ThumbSticks.Left.Y < 0))
            {
                foreach (var star in starsCollection)
                {
                    star.MoveDown(gameTime);
                }

                player.MoveDown(gameTime);
            }

            if((kState.IsKeyDown(Keys.Space) || gState.Buttons.A == ButtonState.Pressed) && validBullet > 9 && playerBullets.Count < 3)
            {
                validBullet = 0;
                playerBullets.Add(new Bullet(player.Position_X, player.Position_Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            //TODO move sprite images to class and interface.
            //Draw starfield
            foreach (var star in movingObjects)
            {
                Texture2D starToUse = null;
                switch (star.Position_Z)
                {
                    case 1:
                        starToUse = star1;
                        break;
                    case 2:
                        starToUse = star2;
                        break;
                    case 3:
                        starToUse = star3;
                        break;
                }

                _spriteBatch.Draw(starToUse, new Vector2(star.Position_X, star.Position_Y), Color.White);
            }

            _spriteBatch.Draw(playerImage, new Vector2(player.Position_X, player.Position_Y), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
