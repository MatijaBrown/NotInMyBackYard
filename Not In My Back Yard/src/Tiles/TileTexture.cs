using NIMBY.Graphics;

namespace NIMBY.Tiles
{
    public static class TileTexture
    {

        public static Texture Grass, Water, Forest, House, Mountain, Building;
        public static Texture Turbine;

        public static void Init()
        {
            Grass = ResourceManager.LoadTexture("Basic_Tile_transparent_green");
            Water = ResourceManager.LoadTexture("Basic_Tile_transparent_blue");
            Forest = ResourceManager.LoadTexture("forest1");
            House = ResourceManager.LoadTexture("house1_lights_off");
            Mountain = ResourceManager.LoadTexture("mountain1");
            Building = ResourceManager.LoadTexture("building1_lights_on");
            Turbine = ResourceManager.LoadTexture("fan_blades_static");
        }

        public static Texture Get(TileType type)
        {
            return type switch
            {
                TileType.Grass => Grass,
                TileType.Water => Water,
                TileType.Forest => Forest,
                TileType.House => House,
                TileType.Mountain => Mountain,
                TileType.Building => Building,
                _ => throw new System.Exception("This tile type does not exist!"),
            };
        }

    }
}
