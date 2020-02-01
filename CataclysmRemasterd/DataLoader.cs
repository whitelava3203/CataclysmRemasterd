using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using static DataStructure;
using Microsoft.Xna.Framework.Graphics;
using CataclysmRemasterd;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using ObjectSaver;

namespace CataclysmRemasterd
{
    public class DataLoader// : MonoBehaviour
    {

        public List<DirectoryInfo> ModFolderDiList = new List<DirectoryInfo>();

        public void LoadMods(ref DataStorage datastorage)
        {
            //Debug.Log(System.Environment.CurrentDirectory);
            LoadMod();
        }
        private List<string> LoadModList()
        {

            ModFolderDiList = new List<DirectoryInfo>();
            DirectoryInfo di = new DirectoryInfo(Staticuse.ModPath);
            List<string> str2 = new List<string>();
            if (di.Exists)
            {
                DirectoryInfo[] CInfo = di.GetDirectories("*", System.IO.SearchOption.TopDirectoryOnly);
                foreach (DirectoryInfo info in CInfo)
                {
                    ModFolderDiList.Add(info);
                }


                if (ModFolderDiList.Count == 0)
                {
                    //Debug.Log(@"(DataLoader.LoadModList)오류/모드가 없습니다.");
                }
                else
                {
                    foreach (DirectoryInfo di2 in ModFolderDiList)
                    {
                        str2.Add(di2.FullName);
                    }
                    //Debug.Log(@"(DataLoader.LoadModList)로그/로드성공.");
                    return str2;
                }
            }
            else
            {
                //Debug.Log(@"(DataLoader.LoadModList)오류/" + ModPath + " 모드 폴더를 찿을수 없습니다.");
            }


            //Debug.Log(@"(DataLoader.LoadModList)오류/로드실패.");
            return new List<String>();
        }

        private void LoadMod()
        {
            List<string> ModFolderPathList = LoadModList();
            foreach (string ModFolderPath in ModFolderPathList)
            {
                LoadSingleMod(ModFolderPath);
            }

        }



        private void LoadSingleMod(string ModFolderPath)
        {
            string path1 = Path.Combine(ModFolderPath, "Modinfo.dat");
            string str = "";
            if (File.Exists(path1))
            {
                str = File.ReadAllText(path1);
            }
            else
            {
                //Debug.Log(@"(DataLoader.LoadSingleMod)오류/" + path1 + " 모드인포 파일을 찾을수 없습니다.");
                return;
            }
            ModInfo info = ObjectSaver.ObjectSaver.Load<ModInfo>(path1);
            byte[] scriptdata = File.ReadAllBytes(Path.Combine(ModFolderPath,info.ScriptPath));

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, "");
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                }

                throw new InvalidOperationException(sb.ToString());
            }
            Assembly assembly = results.CompiledAssembly;

            string typeloc = info.Author + "." + info.ModName;

            Type program = assembly.GetType(typeloc);
            object obj = assembly.CreateInstance(typeloc);



            program.GetField("data").SetValue(obj, Staticuse.mainstorage);
            program.GetMethod("Initialize").Invoke(obj, null);
            DataLoadScript loader = program.GetField("load").GetValue(obj) as DataLoadScript;
            Staticuse.mainstorage.LoadedModAssembly.Add(assembly);


            DataLoadScriptLoader.Load(loader);
        }
        private string Zip(string str)
        {
            var rowData = Encoding.UTF8.GetBytes(str);
            byte[] compressed = null;
            using (var outStream = new MemoryStream())
            {
                using (var hgs = new GZipStream(outStream, CompressionMode.Compress))
                {
                    //outStream에 압축을 시킨다.
                    hgs.Write(rowData, 0, rowData.Length);
                }
                compressed = outStream.ToArray();
            }

            return Convert.ToBase64String(compressed);
        }
        private string UnZip(string str)
        {
            string output = null;
            byte[] cmpData = Convert.FromBase64String(str);
            using (var decomStream = new MemoryStream(cmpData))
            {
                using (var hgs = new GZipStream(decomStream, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(hgs))
                    {
                        output = reader.ReadToEnd();
                    }
                }
            }
            return output;
        }
        
    }
    public static class DataLoadScriptLoader
    {



        public static void Load(DataLoadScript loader)
        {

            foreach (Func<DataStructure.Map.Tile> tileload in loader.TileList)
            {
                Map.Tile tile = tileload();
                Staticuse.mainstorage.TileStorage.Add(tile);
                _=tile.sprite;
            }
            foreach (Func<DataStructure.Map.Material> materialload in loader.MaterialList)
            {
                Staticuse.mainstorage.MaterialStorage.Add(materialload());
            }
            foreach (Func<DataStructure.Map.BaseChunk> basechunkload in loader.BaseChunkList)
            {
                Staticuse.mainstorage.BaseChunkStorage.Add(basechunkload());
            }

        }
    }

    public class ModInfo
    {
        public string Author;
        public string ModName;
        public string ScriptPath = "Script.cs";
        public bool BlockDanagerScript = true;
    }
}