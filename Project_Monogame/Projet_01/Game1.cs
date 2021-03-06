﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projet_01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GameObject Fond;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject Hero;
        Rectangle fenetre;
        GameObject Monstre;
        //pour le déplacement du personnage
        int yTaileEcran;
        int xTaileEcran;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            xTaileEcran = graphics.GraphicsDevice.DisplayMode.Height;
            yTaileEcran = graphics.GraphicsDevice.DisplayMode.Width;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Fond = new GameObject();
            Fond.sprite = Content.Load<Texture2D>("Fond.png");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new GameObject();
            Hero.EstVivant = true;
            Hero.position.X = 0;
            Hero.position.Y = 0;
            Hero.sprite = Content.Load<Texture2D>("astronauta.png");

            Monstre = new GameObject();
            Monstre.EstVivant = true;
            Monstre.position.X = 1600;
            Monstre.position.Y = 200;
            Monstre.sprite = Content.Load<Texture2D>("Monstre.png");

            // TODO: use this.Content to load your game content here
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
            //Déplacement de personnages
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Hero.position.X -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Hero.position.X += 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Hero.position.Y -= 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Hero.position.Y += 2;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
            UpdateHero();
            UpdateMonstre();
        }
        public void UpdateHero()
        {
            
            //Faire apparaite le personnage de l'autre côter de l'écran....
            if (Hero.position.X > 1900)
            {
                Hero.position.X = -100;
            }
            if(Hero.position.X < -100)
            {
                Hero.position.X = 1900;
            }
            if(Hero.position.Y > 1080)
            {
                Hero.position.Y = -100;
            }
            if(Hero.position.Y < -100)
            {
                Hero.position.Y = 1080;
            }
            
        }
        public void UpdateMonstre()
        {
            Random Xmove = new Random();
            Random Ymove = new Random();

            
           
            



        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Fond.sprite, Fond.position, Color.White);
            spriteBatch.Draw(Hero.sprite, Hero.position, Color.White);
            spriteBatch.Draw(Monstre.sprite, Monstre.position, Color.White);


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
