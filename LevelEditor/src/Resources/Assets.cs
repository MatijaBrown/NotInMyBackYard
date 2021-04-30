using Silk.NET.OpenGL.Legacy;

namespace Wind_Thing.Resources
{
    public static class Assets
    {

        public static Texture GrassTexture, WaterTexture, ForestTexture, HouseTexture, MountainTexture, Building;
        public static Texture Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine;
        public static Texture Turbine;

        public static void Init(GL gl)
        {
            GrassTexture = new Texture("Basic_Tile_green", gl);
            WaterTexture = new Texture("Basic_Tile_transparent_blue", gl);
            ForestTexture = new Texture("forest1", gl);
            HouseTexture = new Texture("house1_lights_off", gl);
            MountainTexture = new Texture("mountain1", gl);
            Building = new Texture("building1_lights_on", gl);

            Zero = new Texture("nums/0", gl);
            One = new Texture("nums/1", gl);
            Two = new Texture("nums/2", gl);
            Three = new Texture("nums/3", gl);
            Four = new Texture("nums/4", gl);
            Five = new Texture("nums/5", gl);
            Six = new Texture("nums/6", gl);
            Seven = new Texture("nums/7", gl);
            Eight = new Texture("nums/8", gl);
            Nine = new Texture("nums/9", gl);

            Turbine = new Texture("fan_blades_static", gl);
        }

        public static void Dispose()
        {
            GrassTexture.Dispose();
            WaterTexture.Dispose();
            ForestTexture.Dispose();
            HouseTexture.Dispose();
            MountainTexture.Dispose();
            Building.Dispose();

            Zero.Dispose();
            One.Dispose();
            Two.Dispose();
            Three.Dispose();
            Five.Dispose();
            Six.Dispose();
            Eight.Dispose();
            Nine.Dispose();

            Turbine.Dispose();
        }

    }
}
