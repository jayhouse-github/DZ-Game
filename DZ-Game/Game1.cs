using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DZGame.GameObjects;
using System.Collections;
using System.Collections.Generic;

namespace DZ_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int screenHeight = 768;
        private const int screenWidth = 1024;
        private const int starCount = 70;

        Texture2D star1;
        Texture2D star2;
        Texture2D star3;
        ICollection<Star> starsCollection;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            starsCollection = new List<Star>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            //Populate stars
            for (int i = 0; i < starCount; i++)
            {
                starsCollection.Add(new Star(screenWidth, screenHeight));   
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            star1 = Content.Load<Texture2D>("star1");
            star2 = Content.Load<Texture2D>("star2");
            star3 = Content.Load<Texture2D>("star3");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            //Draw starfield
            foreach (var star in starsCollection)
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

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
