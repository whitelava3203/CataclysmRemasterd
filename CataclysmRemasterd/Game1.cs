using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ObjectSaver;
using System;
using System.Collections.Generic;
using static CataclysmRemasterd.DataStructure.Map;

namespace CataclysmRemasterd
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager gdm;
        private SpriteBatch graphic;
        private OverMapGraphics mapgraphic = new OverMapGraphics();
        private SpriteFont _spriteFont;
        MouseState mouse = Mouse.GetState();
        Texture2D spr;
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

            spr = StaticUse.mainstorage.TileStorage["whitelava3203.MainData/tile/floor/grass"].sprite;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                mapgraphic.Position.X -= 3;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                mapgraphic.Position.X += 3;
            }


            //gdm.PreferredBackBufferWidth = rand.Next(30,700);  // set this value to the desired width of your window
            //gdm.PreferredBackBufferHeight = rand.Next(30, 700);   // set this value to the desired height of your window
            //gdm.ApplyChanges();



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapgraphic.CameraDraw(spr,new Vector2(70,70));
            

            
            graphic.Begin();
            mouse = Mouse.GetState();
            graphic.DrawString(_spriteFont, mouse.X + " : " + mouse.Y, new Vector2(100, 100), Color.White);
            graphic.End();

            base.Draw(gameTime);
        }

        

        
    }
}
