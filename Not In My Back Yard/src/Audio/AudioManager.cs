using System.Diagnostics;

namespace NIMBY.Audio
{
    public static class AudioManager
    {

        private static volatile Process current;
        private static volatile Process lastEffect;

        public static void PlayAndWait(string file)
        {
            current = Process.Start(@"powershell", $@"-c (New-Object Media.SoundPlayer '{file}').PlaySync();");
            current.WaitForExit();
        }


        public static void Play(string file)
        {
            lastEffect = Process.Start(@"powershell", $@"-c (New-Object Media.SoundPlayer '{file}').PlaySync();");
        }


        public static void Stop()
        {
            if (current != null)
            {
                current.Kill();
            }
        }

        public static void Finish()
        {
            if (lastEffect != null)
                lastEffect.Kill();
        }

    }
}
