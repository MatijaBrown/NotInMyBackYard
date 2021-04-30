namespace Wind_Thing
{
    class Program
    {

        static void Main()
        {
            Game game = new(1280, 960, "Wind_Thing");
            game.Run();
            game.Dispose();
        }

    }
}
