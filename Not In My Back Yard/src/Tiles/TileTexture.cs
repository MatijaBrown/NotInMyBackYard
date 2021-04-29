using NIMBY.Graphics;

namespace NIMBY.Tiles
{
    public static class TileTexture
    {

        public static Texture Grass, Water, Forest, House, Mountain;
        public static Texture Turbine;

        static TileTexture()
        {
            Grass = ResourceManager.LoadTexture("Basic_Tile_transparent_green");
            Water = ResourceManager.LoadTexture("Basic_Tile_transparent_blue");
            Forest = ResourceManager.LoadTexture("forest1");
            House = ResourceManager.LoadTexture("house1_lights_off");
            Mountain = ResourceManager.LoadTexture("mountain1");
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
                _ => throw new System.Exception("This tile type does not exist!"),
            };
        }

    }
}
