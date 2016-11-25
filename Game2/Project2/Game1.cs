using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Project2
{


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GameObject Fond;
        GameObject Hero;
        Rectangle fenetre;
        GameObject[] Monstre = new GameObject[5];
        GameObject[] Feu = new GameObject[5];
        GameObject laser;
        GameObject Dead;


        //pour le déplacement du personnage
        int yTaileEcran;
        int xTaileEcran;

        //Nb de vie
        int vie = 3;

        //compteur
        int compteur = 0;
        

        //pour un déplacement Random
        Random de = new Random();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

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

            spriteBatch = new SpriteBatch(GraphicsDevice);
            laser = new GameObject();
            laser.EstVivant = true;
            laser.position.X = 800;
            laser.position.Y = 900;
            laser.sprite = Content.Load<Texture2D>("laser.png");






            for (int i = 0; i < Monstre.Length; i++)
            {
                Monstre[i] = new GameObject();
                Monstre[i].sprite = Content.Load<Texture2D>("Enemy.png");
                Monstre[i].EstVivant = true;
                Monstre[i].position.X = de.Next(fenetre.Width);
                Monstre[i].position.Y = 50;
                Monstre[i].vitesse.X = de.Next(-30, 30); 

            }


            for (int i = 0; i < Feu.Length; i++)
            {
 
                Feu[i] = new GameObject();
                Feu[i].sprite = Content.Load<Texture2D>("Boule_de_feu.png");
                Feu[i].EstVivant = true;
                Feu[i].vitesse.Y = de.Next(14, 15);

            

}
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
            Updatelaser();
        }
        public void UpdateHero()
        {

            //Faire apparaite le personnage de l'autre côter de l'écran....
            if (Hero.position.X > fenetre.Width)
            {
                Hero.position.X = -100;
            }
            if (Hero.position.X < -100)
            {
                Hero.position.X = fenetre.Width;
            }

            if (Hero.position.Y + Hero.sprite.Height >= fenetre.Height)
            {
                Hero.position.Y = fenetre.Height - Hero.sprite.Height;
            }
            if (Hero.position.Y <= fenetre.Y)
            {
                Hero.position.Y = 0;
            }
            for (int i = 0; i < Feu.Length; i++)
            { 
                if (Hero.GetRect().Intersects(Feu[i].GetRect()))
            {
                Hero.EstVivant = false;
            }
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





            for (int i = 0; i < Monstre.Length; i++)
            {

                Monstre[i].position.X += Monstre[i].vitesse.X;

                if (Monstre[i].position.X > fenetre.Width)
            {
                Monstre[i].position.X = -100;
            }
            if (Monstre[i].position.X < -100)
            {
                Monstre[i].position.X = fenetre.Width;
            }
            if (Monstre[i].position.Y > fenetre.Height)
            {
                Monstre[i].position.Y = -100;
            }
            if (Monstre[i].position.Y < -100)
            {
                Monstre[i].position.Y = fenetre.Height;
            }
            if (Hero.EstVivant == false)
            {
                Monstre[i].position.X = de.Next(fenetre.Width);
                Monstre[i].position.Y = 50;

            }
}
        }
        public void UpdateFeu()
        {
            for (int i = 0; i < Feu.Length; i++)
            { 

                Feu[i].position += Feu[i].vitesse;

 
            if (Feu[i].position.Y > fenetre.Height)
            {
                Feu[i].position.Y = Monstre[i].position.Y + 50;
                Feu[i].position.X = Monstre[i].position.X;

            }
            if (Hero.EstVivant == false)
            {

                Feu[i].position.X = 1600;
                Feu[i].position.Y = 100;
            }

}


        }
        public void Updatelaser()
        {
        
        
        
        
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
            spriteBatch.Draw(laser.sprite, laser.position, Color.White);
            spriteBatch.Draw(Hero.sprite, Hero.position, Color.White);

            for (int i = 0; i < Feu.Length; i++)
            {
            spriteBatch.Draw(Feu[i].sprite, Feu[i].position, Color.White);
            }
            for (int i = 0; i < Monstre.Length; i++)
            { 
                spriteBatch.Draw(Monstre[i].sprite, Monstre[i].position, Color.White);
}

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
