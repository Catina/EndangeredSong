﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using OpenTK;
using System;
using System.Collections;
using System.Diagnostics;

namespace EndangeredSong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Controls controls;

        Menu menu;
        bool started;
        Camera camera;
        ArrayList undiscoveredHarmonians;
        ArrayList obstacles;
        Harmonian player;
        BIOAgent b1;
        Random rand;
        int dimX;
        int dimY;
        int screenWidth;
        int screenHeight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Endangered Song";
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
            IsMouseVisible = false;
            camera = new Camera(GraphicsDevice.Viewport);
            GraphicsDevice.Viewport = new Viewport(0, 0, 2000, 1800);
            screenWidth = 980;
            screenHeight = 540;
            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window          
            graphics.ApplyChanges();
            dimX = GraphicsDevice.Viewport.Bounds.Width;
            dimY = GraphicsDevice.Viewport.Bounds.Height;

            undiscoveredHarmonians = new ArrayList();
            obstacles = new ArrayList();

            player = new Harmonian(300, 250, 200, 125, dimX, dimY);
            b1 = new BIOAgent(600, 300, 50, 50, dimX, dimY);
            menu = new Menu(0, 0, 980, 540);

            started = false;
            
            controls = new Controls();
            rand = new Random();
            

            for (int i = 0; i < 10; i++)    //randomly generate 10 obstacles and harmonians on the map
            {
                Harmonian h = new Harmonian(rand.Next(0, 1800), rand.Next(0, 1600), 200, 125, dimX, dimY);
                Obstacle o = new Obstacle(rand.Next(0, 1800), rand.Next(0, 1600), 400, 300);
                undiscoveredHarmonians.Add(h);
                obstacles.Add(o);
            }
           

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.LoadContent(this.Content);
            for (int i = 0; i < 10; i++)
            {
                ((Harmonian)undiscoveredHarmonians[i]).LoadContent(this.Content);
                ((Obstacle)obstacles[i]).LoadContent(this.Content);
            }

            b1.LoadContent(this.Content);
            menu.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                started = true;
            }

            if (started)
            {
                camera.Update(gameTime, player, screenWidth, screenHeight);
                player.Update(controls, gameTime);

                for (int i = 0; i < 10; i++)
                {
                    //((Harmonian)undiscoveredHarmonians[i]).Update(controls, gameTime);
                    ((Obstacle)obstacles[i]).Update(controls, gameTime);
                }
                b1.Update(controls, gameTime, player);
            }
            else
            {
                menu.Update();
                camera.Update(gameTime, menu, screenWidth, screenHeight);
            }

            controls.Update();
                             
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            
            if (!started)
                menu.Draw(spriteBatch);
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    ((Harmonian)undiscoveredHarmonians[i]).Draw(spriteBatch);
                    ((Obstacle)obstacles[i]).Draw(spriteBatch);
                }
                b1.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
