using NIMBY.Audio;

namespace NIMBY
{
    class Program
    {

        public const uint START_WIDTH = 1280, START_HEIGHT = 960;

        static void Main()
        {
            Game game = new(START_WIDTH, START_HEIGHT, "Not In My Back Yard!");
            game.Run();
            game.Dispose();
        }
    }
}
