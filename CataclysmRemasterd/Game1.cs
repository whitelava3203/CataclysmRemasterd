using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ObjectSaver;
using System;

namespace CataclysmRemasterd
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager gdm;
        private SpriteBatch graphic;
        private OverMapGraphics mapgraphic = new OverMapGraphics();
        private SpriteFont _spriteFont;

        public Game1()
        {



            gdm = new GraphicsDeviceManager(this);
            //Staticuse.gdm = _graphicsDeviceManager;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gdm.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            gdm.PreferredBackBufferHeight = 450;   // set this value to the desired height of your window
            gdm.ApplyChanges();
            
            DataLoader.LoadMods();

        }

        protected override void LoadContent()
        {
            graphic = new SpriteBatch(GraphicsDevice);
            mapgraphic.batch = graphic;
            _spriteFont = Content.Load<SpriteFont>("Arial");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            
            
            //gdm.PreferredBackBufferWidth = rand.Next(30,700);  // set this value to the desired width of your window
            //gdm.PreferredBackBufferHeight = rand.Next(30, 700);   // set this value to the desired height of your window
            //gdm.ApplyChanges();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //mapgraphic.CameraDraw();
            


            graphic.Begin();
            graphic.DrawString(_spriteFont, "Hello MonoGame!", new Vector2(1000, 100), Color.White);
            graphic.End();

            base.Draw(gameTime);
        }
    }
}
