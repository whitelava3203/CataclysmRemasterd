using System.Collections;
using System.Collections.Generic;
using System;
using CataclysmRemasterd;
using static DataStructure;

namespace whitelava3203
{
    public class MainData
    {
        DataLoadScript load = new DataLoadScript();
        DataStorage data;//this is ref
        void Initialize()
        {
            load.MaterialList.Add(() =>
            {
                Map.Material material = new Map.Material();
                material.CodeName = @"main/material/none";
                material.Name["KOR"] = "없음";
                material.Name["ENG"] = "None";
                material.Explanation["KOR"] = "재료 없음";
                material.Explanation["ENG"] = "No Material";
                return material;
            });
            load.MaterialList.Add(() =>
            {
                Map.Material material = new Map.Material();
                material.CodeName = @"main/material/wood";
                material.Name["KOR"] = "나무";
                material.Name["ENG"] = "Wood";
                material.Explanation["KOR"] = "불에 잘타는 나무";
                return material;
            });

            //Material load end

            load.TileList.Add(() =>
            {
                Map.Tile tile = new Map.Tile();
                tile.CodeName = @"main/tile/floor/empty";
                tile.Name["KOR"] = "빈칸";
                tile.Name["ENG"] = "Empty";
                tile.Explanation["KOR"] = "설명";
                tile.Explanation["ENG"] = "explain";
                tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
                tile.DeathHelp["ENG"] = "help when died by this tile";
                tile.ImagePath = @"main\graphic\tile\floor\empty.png";
                tile.Priority = Drawable.EPriority.Floor;
                tile.Attribute.Add("PlayerPassable", true);
                tile.Attribute.Add("LightPassable", true);
                tile.Event.Add("Update", () =>
                {

                });
                tile.Event.Add("PlayerOnTile", () =>
                {

                });
                return tile;
            });
            load.TileList.Add(() =>
            {
                Map.Tile tile = new Map.Tile();
                tile.CodeName = @"main/tile/floor/grass";
                tile.Name["KOR"] = "잔디";
                tile.Explanation["KOR"] = "설명";
                tile.DeathHelp["KOR"] = "이타일효과로 뒤졌을때 뜨는 도움말";
                tile.ImagePath = @"main\graphic\tile\floor\grass.png";
                tile.Priority = Drawable.EPriority.Floor;
                tile.Attribute.Add("PlayerPassable", true);
                tile.Attribute.Add("LightPassable", true);
                return tile;
            });

            //Tile load end
        }
    }
}