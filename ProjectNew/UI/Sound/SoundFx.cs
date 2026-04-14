using ProjectNew.Core.Models;

namespace ProjectNew.UI.Sound
{
    public static class SoundFx
    {
        public static AppSettings Settings { get; set; } = new AppSettings();

        public static void Click()
        {
            if (!Settings.EnableSound) return;
            BeepSafe(800, 30);
        }

        public static void Correct()
        {
            if (!Settings.EnableSound) return;
            BeepSafe(1046, 70);
            BeepSafe(1318, 70);
        }

        public static void Wrong()
        {
            if (!Settings.EnableSound) return;
            BeepSafe(220, 160);
        }

        public static void Start()
        {
            if (!Settings.EnableSound) return;
            BeepSafe(660, 60);
            BeepSafe(880, 60);
        }

        public static void Finish()
        {
            if (!Settings.EnableSound) return;
            BeepSafe(784, 70);
            BeepSafe(988, 70);
            BeepSafe(1318, 90);
        }

        private static void BeepSafe(int frequency, int durationMs)
        {
            try { Console.Beep(frequency, durationMs); }
            catch { /* Some environments don't allow beeps */ }
        }
    }
}
