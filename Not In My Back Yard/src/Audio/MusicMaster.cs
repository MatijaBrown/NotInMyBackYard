using System.Threading;

namespace NIMBY.Audio
{

    public enum MusicState
    {
        Game, Menu
    }

    public static class MusicMaster
    {

        private static volatile MusicState state = MusicState.Menu;
        private static volatile bool running = true;

        public static MusicState State { set => state = value; }

        public static bool Running { set => running = value; }

        public static void Start()
        {
            Thread thread = new Thread(() =>
            {
                while (running)
                {
                    switch (state)
                    {
                        case MusicState.Game:
                            AudioManager.PlayAndWait("./Assets/Sound/Game Music Loop.wav");
                            break;
                        case MusicState.Menu:
                            AudioManager.PlayAndWait("./Assets/Sound/Menu Music Loop.wav");
                            break;
                    }
                }
                AudioManager.Stop();
            });
            thread.Start();
        }

    }
}
