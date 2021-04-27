namespace NIMBY
{
    class Program
    {
        static void Main()
        {
            Game game = new(800, 720, "Not In My Back Yard!");
            game.Run();
            game.Dispose();
        }
    }
}
