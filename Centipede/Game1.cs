using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Centipede
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Saved so they don't have to be loaded every time sprites are created
        static public Texture2D pointSprite;
        static public Texture2D bulletSprite;
        static public Texture2D playerSprite;
        static public Texture2D mushroomSprite;

        Player player;
        Mushroom mushroom;
        MushroomGrid mushroomGrid;

        static List<Bullet> bullets = new List<Bullet>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GameConstants.WindowWidth;
            graphics.PreferredBackBufferHeight = GameConstants.WindowHeight;
            //IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // create a new SpriteBatch, which can be used to draw textures
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load sprites
            pointSprite = Content.Load<Texture2D>(@"graphics\point");
            bulletSprite = Content.Load<Texture2D>(@"graphics\bullet");
            playerSprite = Content.Load<Texture2D>(@"graphics\player");
            mushroomSprite = Content.Load<Texture2D>(@"graphics\mushroom");

            // add initial game objects
            player = new Player(spriteBatch, playerSprite, 24, 24,
                new Vector2(GameConstants.WindowWidth / 2 - 12, GameConstants.WindowHeight - 12));
            mushroom = new Mushroom(spriteBatch, mushroomSprite, 24, 24, Vector2.Zero);
            mushroomGrid = new MushroomGrid(spriteBatch, mushroom);

            Mouse.SetPosition(player.Rectangle.Center.X, player.Rectangle.Center.Y);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }

            MouseState mouseState = Mouse.GetState();

            player.Update(gameTime, mouseState, mushroomGrid);

            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime);
            }

            // clean out inactive bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (!bullets[i].Active)
                {
                    bullets.RemoveAt(i);
                }
            }
            //Console.WriteLine(bullets.Count);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            player.Draw();
            mushroomGrid.Draw();
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public static void AddBullet(Bullet bullet)
        {
            bullets.Add(bullet);
        }
    }
}
