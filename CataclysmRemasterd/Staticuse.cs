using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CataclysmRemasterd
{
    public class Staticuse
    {
        public static GraphicsDeviceManager gdm;
        public static DataStorage mainstorage = new DataStorage();
        public static GraphicsDevice gd = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
        public static string ModPath
        {
            get
            {
                return Path.Combine(System.Environment.CurrentDirectory,"Data");
            }
        }
    }
}
