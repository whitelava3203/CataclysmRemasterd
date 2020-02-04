using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataStructure;

namespace CataclysmRemasterd
{
    class OverMapGraphics
    {
        public Vector2 Position = new Vector2();
        public Point ScreenPixelSize = new Point();
        public float Zoom = 1f;



        public SpriteBatch batch;// = new SpriteBatch(Staticuse.gd);
        public void CameraDraw(Texture2D sprite, Vector2 Pos)
        {
            Vector2 v2 = new Vector2();
            v2 = this.Position - Pos;
            v2 = v2 / Zoom;
            Point v3 = new Point();
            v3.X = (int)((v2.X - (sprite.Width / 2))*Zoom);
            v3.Y = (int)((v2.Y - (sprite.Height / 2))*Zoom);
            Point v4 = new Point();
            v4.X = (int)((v2.X + (sprite.Width / 2))*Zoom);
            v4.Y = (int)((v2.Y + (sprite.Height / 2))*Zoom);

            batch.Draw(sprite,
               new Rectangle(v3,v4),
               new Rectangle(0, 0, sprite.Width, sprite.Height),
               Color.White);
        }


        void asd()
        {



            //batch.Draw(null);
        }

        



    }
}
