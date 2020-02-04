using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CataclysmRemasterd;
using System.Reflection;

public class DataStructure
{
    public class IntPos
    {
        public bool CheckPos(Point pos)
        {
            if (pos == Position) return true;
            else return false;
        }
        protected Point _Position;
        public virtual Point Position
        {
            get
            {
                return _Position;
            }
            set
            {
                _Position = value;
            }
        }

        public Point FixedPosition
        {
            get
            {
                Point v = new Point();
                v.X = Position.X;
                v.Y = Position.Y;
                return v;
            }
            set
            {
                _Position.X = value.X;
                _Position.Y = value.Y;
            }
        }
    }




    
    public class Map
    {

        public class Chunk : List<Map.TileContainer>
        {
            public ChunkContainer UpperContainer;
            new public void Add(Map.TileContainer tilecontainer)
            {
                tilecontainer.UpperChunk = this;
                base.Add(tilecontainer);
            }

        }
        public class ChunkContainer : IntPos
        {
            private Map.Chunk _TileContainerList;
            public Map.Chunk TileContainerList
            {
                get
                {
                    return _TileContainerList;
                }
                set
                {
                    _TileContainerList = value;
                    _TileContainerList.UpperContainer = this;
                }
            }

            public Map.WorldMap UpperWorldMap;

            public ChunkContainer()
            {

            }
            public ChunkContainer(Chunk chunk1)
            {
                TileContainerList = chunk1;
                chunk1.UpperContainer = this;
            }
            public ChunkContainer(Chunk chunk1, Point pos)
            {
                TileContainerList = chunk1;
                Position = pos;
                chunk1.UpperContainer = this;
            }

            public Map.TileContainer FindTileWithPos(Point pos)
            {
                foreach(Map.TileContainer tilecontainer in TileContainerList)
                {
                    if (tilecontainer.CheckPos(pos))
                        return tilecontainer;
                }
                return null;
            }
            public List<Map.TileContainer> FindAllTileWithPos(Point pos)
            {
                Chunk chunk1 = new Chunk();
                foreach (Map.TileContainer tilecontainer in TileContainerList)
                {
                    if (tilecontainer.CheckPos(pos))
                        chunk1.Add(tilecontainer);
                }
                if (chunk1.Count > 0)
                    return chunk1;
                else
                    return null;
            }
            public List<Map.TileContainer> FindAllTileWithTile(Map.TileContainer tilecontainer)
            {
                return FindAllTileWithPos(tilecontainer.Position);
            }

            public void AddTileContainer(Map.TileContainer tilecontainer)
            {
                this.TileContainerList.Add(tilecontainer);
            }
            public bool RemoveTileContainer(Map.TileContainer tilecontainer)
            {
                return this.TileContainerList.Remove(tilecontainer);
            }
            
        }
        public class WorldMap
        {
            public List<ChunkContainer> ChunkContainerList = new List<ChunkContainer>();


            public TileContainer FindTileWithPos(Point pos)
            {
                return FindChunkWIthPos(pos).FindTileWithPos(pos);
            }
            
            public ChunkContainer FindChunkWIthPos(Point pos)
            {
                pos.X /= 24;
                pos.Y /= 24;
                foreach(ChunkContainer chunkcontainer in ChunkContainerList)
                {
                    if(chunkcontainer.CheckPos(pos))
                    {
                        return chunkcontainer;
                    }
                }
                return null;
            }
        }
        public class BaseChunk : CodeObject
        {
            public List<ChunkContainer> ChunkContainerList = new List<ChunkContainer>();

            override public string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("        Load.BaseChunkList.Add(() =>");
                sb.AppendLine("        {");
                sb.AppendLine("            Map.BaseChunk basechunk = new Map.BaseChunk();");
                sb.AppendLine("            Map.ChunkContainer cc;");
                sb.AppendLine("            Map.TileContainer tc;");
                sb.AppendLine("            basechunk.CodeName = \"" + this.CodeName + "\";");
                foreach (ChunkContainer chunkcontainer in this.ChunkContainerList)
                {
                    sb.AppendLine("            cc = new Map.ChunkContainer();");
                    foreach (TileContainer tilecontainer in chunkcontainer.TileContainerList)
                    {
                        sb.AppendLine("            tc = new Map.TileContainer();");
                        sb.AppendLine("            tc.Data = Storage.TileStorage[\"" + tilecontainer.Data.CodeName + "\"];");
                        sb.AppendLine("            tc.Position = new Point(" + tilecontainer.Position.X + ", " + tilecontainer.Position.Y + ");");
                        sb.AppendLine("            cc.TileContainerList.Add(tc);");
                    }
                    sb.AppendLine("            basechunk.ChunkContainerList.Add(cc);");
                }

                sb.AppendLine("            return basechunk;");

                return sb.ToString();
            }
        }
        public class Tile : Drawable
        {
            
            public LangString Name = new LangString();
            public LangString Explanation = new LangString();
            public LangString DeathHelp = new LangString();
            public Material BaseMaterial = new Material();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string,object> Attribute = new Dictionary<string, object>();

        }
        public class TileContainer : IntPos
        {
            public Chunk UpperChunk;
            public Map.ChunkContainer UpperContainer
            {
                get
                {
                    return UpperChunk.UpperContainer;
                }
            }


            private Map.Tile _Data;
            public Map.Tile Data
            {
                get
                {
                    return _Data;
                }
                set
                {
                    _Data = value;
                    Syncronize();
                }
            }
            public override Point Position
            {
                set
                {
                    _Position = value;
                    Syncronize();
                }
            }

            private void Syncronize()
            {
            }
            
            public TileContainer()
            {
            }
            public TileContainer(Map.Tile tile1)
            {
                Data = tile1;
            }

            public TileContainer(Map.Tile tile1, Point pos)
            {
                Data = tile1;
                Position = pos;
            }
            public TileContainer(Map.Tile tile1, int x,int y)
            {
                Data = tile1;
                Position = new Point(x,y);
            }





        }
        public class Material : CodeObject
        {
            public LangString Name = new LangString();
            public LangString Explanation = new LangString();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string, object> Attribute = new Dictionary<string, object>();
        }

        public class Item : Drawable
        {
            public LangString Name = new LangString();
            public LangString Explanation = new LangString();
            public LangString DeathHelp = new LangString();
            
            public Material Material = new Material();
            public Dictionary<string, Action> Event = new Dictionary<string, Action>();
            public Dictionary<string, object> Attribute = new Dictionary<string, object>();

        }
        public class ItemContainer
        {

        }
    }
}
public class DataLoadScript
{
    public List<Func<DataStructure.Map.Tile>> TileList = new List<Func<DataStructure.Map.Tile>>();
    public List<Func<DataStructure.Map.Material>> MaterialList = new List<Func<DataStructure.Map.Material>>();

    public List<Func<DataStructure.Map.BaseChunk>> BaseChunkList = new List<Func<DataStructure.Map.BaseChunk>>();
}


public class LangString
{
    private Dictionary<string, string> data = new Dictionary<string, string>();

    public static List<string> CurrentLanguage = new List<string>();



    public string this[string lang]
    {
        get
        {
            return data[lang];
        }
        set
        {
            string str;
            if(data.TryGetValue(lang, out str))
                data[lang] = value;
            else
                data.Add(lang, value);
            
        }
    }

    public static implicit operator string(LangString langstr)
    {
        foreach(string lang in CurrentLanguage)
        {
            if(langstr.data[lang] != null)
            {
                return langstr.data[lang];
            }
        }
        return "EMPTY";
    }
}
public class CodeDictionary<T> : Dictionary<string,T> where T : CodeObject
{
    private List<string> codedata = new List<string>();

    public T this[int index]
    {
        get
        {
            return this[codedata[index]];
        }
        set
        {
            this[codedata[index]] = value;
        }
    }
    public void Add(T obj)
    {
        this.Add(obj.CodeName, obj);
        this.codedata.Add(obj.CodeName);
    }
    new public void Clear()
    {
        this.Clear();
        this.codedata.Clear();
    }


}
public class A
{
    private string str = "AAAAAAAA";
    B b = new B();
}
public class B
{
    private string str = "BBBBBBBB";
    A a = new A();
}


/*
public class DrawDictionary<T> : CodeDictionary<T> where T : Drawable
{

    public void Add(T drawobj,DataLoader dataloader, ref DataStorage datastorage)
    {
        this.data.Add(drawobj.CodeName, drawobj);
        this.codedata.Add(drawobj.CodeName);
        dataloader.ImageLoad(drawobj, ref datastorage);
    }
}
*/
public class CodeObject
{
    private string _CodeName;
    public string CodeName
    {
        get
        {
            return this.modInfo.FullName + @"\" + _CodeName;
        }
        set
        {
            _CodeName = value;
        }
    }
    public ModInfo modInfo;
    //public static DataStorage MainStorage;
}
public class Drawable : CodeObject
{
    //public static DataStorage datastorage;

    public enum EPriority
    {
        Floor = 0,
        Wall = 1000,
        Window = 2000,
        Decoration = 3000,
        Unit = 4000
    }
    public enum EDirection
    {
        Straight,
        Clockwise90,
        Clockwise180,
        Clockwise270
    }
    public enum EShape
    {
        Single,
        Full,
        Degree90,
        Straight,
        One,
        Three
    }
    public bool IsShapedImage = false;
    private string _ImagePath;
    public string ImagePath
    {
        get
        {
            return base.modInfo.FullName + "/" + _ImagePath;
        }
        set
        {
            _ImagePath = value;
        }
    }
    public double ImageSize;
    public EPriority Priority;
    public EShape Shape;
    public EDirection Direction;

    public Texture2D sprite
    {
        get
        {
            string str;
            if (this.IsShapedImage == true)
            {
                str = this.CodeName + "_" + ((int)this.Shape).ToString();
            }
            else
            {
                str = this.CodeName;
            }
            if (StaticUse.mainstorage.ImageStorage.ContainsKey(str))
            {
                return StaticUse.mainstorage.ImageStorage[str];
            }
            else
            {
                string str1;
                string str2;
                if (this.IsShapedImage == true)
                {
                    str1 = this.CodeName + "_" + ((int)this.Shape).ToString();
                    str2 = this.ImagePath + "_" + ((int)this.Shape).ToString();
                }
                else
                {
                    str1 = this.CodeName;
                    str2 = this.ImagePath;
                }
                str2 = Path.Combine(StaticUse.ModPath, str2);

                if (StaticUse.mainstorage.ImageStorage.ContainsKey(str1))
                {
                    return StaticUse.mainstorage.ImageStorage[str1];
                }
                else
                {
                    Texture2D sprite = StaticUse.LoadPNG(str2);
                    if (sprite == null)
                    {
                        //Debug.Log(@"오류/파일" + str2 + " 을 찿을수 없습니다.");
                        return null;
                    }
                    else
                    {
                        StaticUse.mainstorage.ImageStorage.Add(str, sprite);
                        return sprite;
                    }

                }
                //Debug.Log(@"(DataLoader.LoadModList)오류/"+this.CodeName+" 이미지가 로딩되지 않음.");
                //return MainStorage.ImageStorage[str];//빈 스프라이트 주는걸로 바꿔야함
            }
        }
    }
}
public class DataStorage
{
    public CodeDictionary<DataStructure.Map.Tile> TileStorage = new CodeDictionary<DataStructure.Map.Tile>();
    public CodeDictionary<DataStructure.Map.Item> ItemStorage = new CodeDictionary<DataStructure.Map.Item>();
    public CodeDictionary<DataStructure.Map.Material> MaterialStorage = new CodeDictionary<DataStructure.Map.Material>();
    public Dictionary<string, Texture2D> ImageStorage = new Dictionary<string, Texture2D>();

    public CodeDictionary<DataStructure.Map.BaseChunk> BaseChunkStorage = new CodeDictionary<DataStructure.Map.BaseChunk>();


    public List<Assembly> LoadedModAssembly = new List<Assembly>();
}





public static class ExtensionMethods
{
    // Deep clone
    public static T DeepClone<T>(this T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }
}