//Ruben Borrero
//0345052
//HW2
//Game Programming
//Professor John Sterling


/* Notes
 * Each cell should be at least 10x10 px
 * Entire Board should be around 568x385px
 * Add Mouse select and deselect
 * Stop and Start
 * Speed (maybe)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HW2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    enum Life
    {
        Alive, Dead
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int UPS = 20; // Updates per second
        public const int FPS = 60; // Frames per second
        public const int INDENT = 10;
        public const int CellSize = 10;
        public const int CellsX = 50;
        public const int CellsY = 50;

        public static bool Pause = true;
        public static bool JustOneGen = false;

        public static Texture2D Pixel;

        public static Texture2D StartBtn;
        public static Vector2 StButtonPos;
        public static Rectangle StartButtonBox;
                
        public static Texture2D StopBtn;
        public static Texture2D StartOrStop;
        
        public static Texture2D ClrBtn;
        public static Vector2 ClrButtonPos;
        public static Rectangle ClrButtonBox;

        public static Texture2D NextBtn;
        public static Vector2 NextBtnPos;
        public static Rectangle NextBtnBox;

        public static Texture2D SpdUpBtn;
        public static Vector2 SpdUpBtnPos;
        public static Rectangle SpdUpBtnBox;

        public static Texture2D SpdDownBtn;
        public static Vector2 SpdDownBtnPos;
        public static Rectangle SpdDownBtnBox;

        public static Vector2 ScreenSize;

        private MouseState mouseState, lastMouseState;

        public static Point mousePos;

        private Grid grid;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / FPS);

            ScreenSize = new Vector2(CellsX, CellsY) * CellSize;

            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;

            IsMouseVisible = true;
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

            grid = new Grid();

            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            StartBtn = Content.Load<Texture2D>(@"Sprites\Start");
            StButtonPos = new Vector2(0, (graphics.GraphicsDevice.Viewport.Height - (StartBtn.Height)));
            StartButtonBox = new Rectangle(0, graphics.GraphicsDevice.Viewport.Height - (StartBtn.Height), StartBtn.Width, StartBtn.Height);            
            StopBtn = Content.Load<Texture2D>(@"Sprites\Stop");

            ClrBtn = Content.Load<Texture2D>(@"Sprites\Clear");
            ClrButtonPos = new Vector2((INDENT * 2) + StartBtn.Width, graphics.GraphicsDevice.Viewport.Height - ClrBtn.Height);
            ClrButtonBox = new Rectangle((INDENT * 2) + StartBtn.Width, graphics.GraphicsDevice.Viewport.Height - ClrBtn.Height, ClrBtn.Width, ClrBtn.Height);

            NextBtn = Content.Load<Texture2D>(@"Sprites\NextGen");
            NextBtnPos = new Vector2(graphics.GraphicsDevice.Viewport.Width - NextBtn.Width, (graphics.GraphicsDevice.Viewport.Height - NextBtn.Height));
            NextBtnBox = new Rectangle(graphics.GraphicsDevice.Viewport.Width - NextBtn.Width, (graphics.GraphicsDevice.Viewport.Height - NextBtn.Height),
                                           NextBtn.Width, NextBtn.Height);
            SpdDownBtn = Content.Load<Texture2D>(@"Sprites\Down");
                        SpdDownBtnPos = new Vector2(graphics.GraphicsDevice.Viewport.Width - (NextBtn.Width + SpdDownBtn.Width), (graphics.GraphicsDevice.Viewport.Height - SpdDownBtn.Height));
                        SpdDownBtnBox = new Rectangle(graphics.GraphicsDevice.Viewport.Width - (NextBtn.Width + SpdDownBtn.Width), (graphics.GraphicsDevice.Viewport.Height - SpdDownBtn.Height),
                                                        SpdDownBtn.Width, SpdDownBtn.Height);

            SpdUpBtn = Content.Load<Texture2D>(@"Sprites\Up");
            SpdUpBtnPos = new Vector2(graphics.GraphicsDevice.Viewport.Width - (NextBtn.Width + SpdUpBtn.Width), (graphics.GraphicsDevice.Viewport.Height - (SpdUpBtn.Height+SpdDownBtn.Height)));
            SpdUpBtnBox = new Rectangle(graphics.GraphicsDevice.Viewport.Width - (NextBtn.Width + SpdUpBtn.Width), (graphics.GraphicsDevice.Viewport.Height - (SpdUpBtn.Height+SpdDownBtn.Height)),
                                            SpdUpBtn.Width, SpdUpBtn.Height);

            


            Pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            lastMouseState = mouseState;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            mousePos = new Point(mouseState.X, mouseState.Y);
            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            //mouseState = Mouse.GetState();
            if( StartButtonBox.Contains(mousePos) && ((mouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) ) {
                Pause = !Pause;
                /* Ugly swap*/
                StartOrStop = StartBtn;
                StartBtn = StopBtn;
                StopBtn = StartOrStop;

                JustOneGen = false;
            }
            if( ClrButtonBox.Contains(mousePos) && ((mouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) ) {
                grid.Clear();
                UPS = 10;
            }

            if( NextBtnBox.Contains(mousePos) && ((mouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) ) {
                grid.IncrementGeneration();
            }
            if( SpdDownBtnBox.Contains(mousePos) && ((mouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)) ) {
                if (UPS > 1)
                    --UPS;
            }
            if (SpdUpBtnBox.Contains(mousePos) && ((mouseState.LeftButton == ButtonState.Pressed) && (lastMouseState.LeftButton == ButtonState.Released)))
            {
                if (UPS < 30)
                    ++UPS;
            }


            base.Update(gameTime);
            grid.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Gray);
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(StartBtn, StButtonPos, Color.White);
            spriteBatch.Draw(ClrBtn, ClrButtonPos, Color.White);
            spriteBatch.Draw(NextBtn, NextBtnPos, Color.White);
            spriteBatch.Draw(SpdUpBtn, SpdUpBtnPos, Color.White);
            spriteBatch.Draw(SpdDownBtn, SpdDownBtnPos, Color.White);
            grid.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
