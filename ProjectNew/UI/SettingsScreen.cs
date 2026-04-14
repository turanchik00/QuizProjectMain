using ProjectNew.Services;
using ProjectNew.UI.Effects;
using ProjectNew.UI.Sound;
using ProjectNew.UI.Theming;

namespace ProjectNew.UI
{
    public static class SettingsScreen
    {
        public static void Show(SettingsService settings)
        {
            while (true)
            {
                ConsoleFx.SafeClear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                ConsoleFx.Typewriter("===== SETTINGS =====", ConsoleColor.Cyan);
                Console.ResetColor();

                var s = settings.Current;
                Console.WriteLine();
                Console.WriteLine($"[1] Animations: {(s.EnableAnimations ? "ON" : "OFF")}");
                Console.WriteLine($"[2] Sound:      {(s.EnableSound ? "ON" : "OFF")}");
                Console.WriteLine($"[3] Anim Speed: {s.AnimationDelayMs} ms (0 = instant)");
                Console.WriteLine($"[4] Clear UI:   {(s.ClearBetweenQuestions ? "ON" : "OFF")}");
                Console.WriteLine($"[5] Cinematic:  {(s.EnableCinematicTransitions ? "ON" : "OFF")}");
                Console.WriteLine($"[6] Theme:      {s.Theme}");
                Console.WriteLine();
                Console.WriteLine("[0] Back");
                Console.Write("\nSeçim: ");

                var key = Console.ReadKey(true).KeyChar;
                if (key == '0') return;

                switch (key)
                {
                    case '1':
                        s.EnableAnimations = !s.EnableAnimations;
                        break;
                    case '2':
                        s.EnableSound = !s.EnableSound;
                        break;
                    case '3':
                        Console.Write("Yeni dəyər (0-40): ");
                        if (int.TryParse(Console.ReadLine(), out int ms))
                        {
                            if (ms < 0) ms = 0;
                            if (ms > 40) ms = 40;
                            s.AnimationDelayMs = ms;
                        }
                        break;
                    case '4':
                        s.ClearBetweenQuestions = !s.ClearBetweenQuestions;
                        break;
                    case '5':
                        s.EnableCinematicTransitions = !s.EnableCinematicTransitions;
                        break;
                    case '6':
                        {
                            int idx = Array.FindIndex(ThemeRegistry.Names, n => n.Equals(s.Theme, StringComparison.OrdinalIgnoreCase));
                            idx = idx < 0 ? 0 : idx;
                            idx = (idx + 1) % ThemeRegistry.Names.Length;
                            s.Theme = ThemeRegistry.Names[idx];
                        }
                        break;
                    default:
                        SoundFx.Wrong();
                        ConsoleFx.ErrorShake("Yanlış seçim!");
                        break;
                }

                settings.Save();
                ConsoleFx.Settings = settings.Current;
                SoundFx.Settings = settings.Current;
                ConsoleUi.Theme = ThemeRegistry.Get(settings.Current.Theme);
                SoundFx.Click();
            }
        }
    }
}

