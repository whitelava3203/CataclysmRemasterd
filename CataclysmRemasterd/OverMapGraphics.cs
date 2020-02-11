using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CataclysmRemasterd.DataStructure.Map;

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
            v2 = Pos - this.Position;
            v2 = v2 / Zoom;
            Point v3 = new Point();
            v3.X = (int)((v2.X-(sprite.Width / 2))*Zoom);
            v3.Y = (int)((v2.Y-(sprite.Height / 2))*Zoom);
            batch.Begin();
            batch.Draw(sprite,
               new Rectangle(v3.X,v3.Y,sprite.Width,sprite.Height),
               new Rectangle(0, 0, sprite.Width, sprite.Height),
               Color.White);

            batch.End();
        }


        private void DrawTiles(List<TileContainer> tilecontainerlist)
        {
            Predicate<TileContainer> InScreen = (tc) =>
            {
                tc.PosVector2.BetweenLength(this.Position);
                return true;
            };

            tilecontainerlist.FindAll(InScreen).ForEach((tc)=>{CameraDraw(tc.Data.sprite,tc.PosVector2);});
        }



    }
}
