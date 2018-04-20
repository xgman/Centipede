﻿using System;

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

        Point point;
        Bullet bullet;
        Player player;
        Mushroom mushroom;
        MushroomGrid mushroomGrid;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            point = new Point(spriteBatch, Content.Load<Texture2D>(@"graphics\point"), 1, 1, Vector2.Zero);
            bullet = new Bullet(spriteBatch, Content.Load<Texture2D>(@"graphics\bullet"), 1, 4, Vector2.Zero);

            player = new Player(spriteBatch, Content.Load<Texture2D>(@"graphics\player"), 24, 24,
                new Vector2(GameConstants.WindowWidth / 2 - 12, GameConstants.WindowHeight - 12), point, bullet);

            mushroom = new Mushroom(spriteBatch, Content.Load<Texture2D>(@"graphics\mushroom"), 24, 24, Vector2.Zero);
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

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
