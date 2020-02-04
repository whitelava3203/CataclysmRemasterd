using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CataclysmRemasterd
{
    public static class StaticUse
    {
        public static Texture2D LoadPNG(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Texture2D sprite = Texture2D.FromStream(StaticUse.gd, fileStream);
            fileStream.Dispose();
            return sprite;
        }


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




        public static void Initalize() 
        {
            ObjectSaver.ObjectSaver.LoadedAssemblies.Add(Assembly.GetExecutingAssembly());
        }
    }
}
