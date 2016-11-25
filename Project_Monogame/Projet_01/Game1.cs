﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Projet_01
{
    /// <summary>
    /// This is th e main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GameObject Fond;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject Hero;
        Rectangle fenetre;
        GameObject Monstre;
        GameObject Feu;
        GameObject Dead;
        //pour le déplacement du personnage
        int yTaileEcran;
        int xTaileEcran;
        int vie = 3;

        Random de = new Random();


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

            Dead = new GameObject();
            Dead.sprite = Content.Load<Texture2D>("Jack.png");
            Dead.EstVivant = true;
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new GameObject();
            Hero.EstVivant = true;
            Hero.position.X = 800;
            Hero.position.Y = 900;
            Hero.sprite = Content.Load<Texture2D>("astronauta.png");

            

            
            Monstre = new GameObject();
            Monstre.EstVivant = true;
            Monstre.position.X = 1600;
            Monstre.position.Y = 0;
           Monstre.sprite = Content.Load<Texture2D>("enemy.png");
            Monstre.vitesse.X = de.Next(-30, 30);

            Feu = new GameObject();
            Feu.EstVivant = true;
            Feu.position.X = 1600;
            Feu.position.Y = 50;
            Feu.sprite = Content.Load<Texture2D>("Boule_de_feu.png");         
            Feu.vitesse.Y = de.Next(60,61);


            //TODO: use this.Content to load your game content here
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
                Hero.position.X -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Hero.position.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Hero.position.Y -= 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Hero.position.Y += 10;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
            UpdateHero();
            UpdateMonstre();
            UpdateFeu();
        }
        public void UpdateHero()
        {
            
            //Faire apparaite le personnage de l'autre côter de l'écran....
            if (Hero.position.X > fenetre.Width)
            {
                Hero.position.X = -100;
            }
            if(Hero.position.X < -100)
            {
                Hero.position.X = fenetre.Width;
            }
          
            if(Hero.position.Y + Hero.sprite.Height >= fenetre.Height)
            {
                Hero.position.Y = fenetre.Height - Hero.sprite.Height;
            }
            if(Hero.position.Y <= fenetre.Y)
            {
                Hero.position.Y = 0;
            }
           if(Hero.GetRect().Intersects(Feu.GetRect()))
                {
                Hero.EstVivant = false;
              }
              
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                Hero.EstVivant = true;
                Hero.position.X = 800;
                Hero.position.Y = 900;
               
            }
            
        }
        public void UpdateMonstre()
        {
         
          
          
           
          
            Monstre.position.X += Monstre.vitesse.X;
            if (Monstre.position.X > fenetre.Width)
            {
                Monstre.position.X = -100;
            }
            if (Monstre.position.X < -100)
            {
                Monstre.position.X = fenetre.Width;
            }
            if (Monstre.position.Y > fenetre.Height)
            {
                Monstre.position.Y = -100;
            }
            if (Monstre.position.Y < -100)
            {
                Monstre.position.Y = fenetre.Height;
            }
            if (Hero.EstVivant == false)
            {
                Monstre.position.X = 1600;
                Monstre.position.Y = 50;
               
            }

        }
        public void UpdateFeu()
        {          

            
            Feu.position += Feu.vitesse;
            
    
     
              if(Feu.position.Y > fenetre.Height)
            {
                  Feu.position.Y = Monstre.position.Y + 50;
                   Feu.position.X = Monstre.position.X;
              
            }
            if (Hero.EstVivant == false)
            {
             
                Feu.position.X = 1600;
                Feu.position.Y = 100;
            }




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
            spriteBatch.Draw(Feu.sprite, Feu.position, Color.White);
            spriteBatch.Draw(Monstre.sprite, Monstre.position, Color.White);


            if (Hero.EstVivant == false)
                { 
            spriteBatch.Draw(Dead.sprite, Dead.position, Color.White);
                }

                


            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
