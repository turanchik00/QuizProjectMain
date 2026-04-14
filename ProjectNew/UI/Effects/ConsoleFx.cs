using ProjectNew.Core.Models;

namespace ProjectNew.UI.Effects
{
    public static class ConsoleFx
    {
        public static AppSettings Settings { get; set; } = new AppSettings();

        public static void SafeClear()
        {
            try { Console.Clear(); }
            catch { /* Some hosts don't have a real console */ }
        }

        public static void Typewriter(string text, ConsoleColor? color = null, bool newLine = true)
        {
            if (!Settings.EnableAnimations || Settings.AnimationDelayMs <= 0)
            {
                if (color.HasValue) Console.ForegroundColor = color.Value;
                if (newLine) Console.WriteLine(text);
                else Console.Write(text);
                Console.ResetColor();
                return;
            }

            if (color.HasValue) Console.ForegroundColor = color.Value;
            foreach (char c in text)
            {
                Console.Write(c);
                Delay(Settings.AnimationDelayMs);
            }
            if (newLine) Console.WriteLine();
            Console.ResetColor();
        }

        public static void Countdown(string label = "Başlayır", int from = 3)
        {
            if (!Settings.EnableAnimations)
            {
                Console.WriteLine($"{label}...");
                return;
            }

            for (int i = from; i >= 1; i--)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{label}: {i}   ");
                Console.ResetColor();
                Delay(350);
                Console.Write("\r");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{label}: START!");
            Console.ResetColor();
            Delay(200);
        }

        public static void LoadingDots(string label, int dots = 3)
        {
            if (!Settings.EnableAnimations)
            {
                Console.WriteLine(label);
                return;
            }

            Console.Write(label);
            for (int i = 0; i < dots; i++)
            {
                Delay(200);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public static void SuccessFlash(string text)
        {
            if (!Settings.EnableAnimations)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ResetColor();
                return;
            }

            var old = Console.ForegroundColor;
            for (int i = 0; i < 2; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Delay(120);
                Console.ForegroundColor = old;
                Delay(80);
            }
            Console.ResetColor();
        }

        public static void ErrorShake(string text)
        {
            if (!Settings.EnableAnimations)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(text);
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            string[] frames =
            {
                "  " + text,
                " " + text,
                text,
                " " + text,
                "  " + text
            };
            foreach (var f in frames)
            {
                Console.Write("\r" + f + "   ");
                Delay(55);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public static void ScreenWipe()
        {
            if (!Settings.EnableAnimations || !Settings.EnableCinematicTransitions)
            {
                SafeClear();
                return;
            }

            int w, h;
            try
            {
                w = Console.WindowWidth;
                h = Console.WindowHeight;
            }
            catch
            {
                SafeClear();
                return;
            }

            for (int y = 0; y < Math.Min(h, 30); y++)
            {
                Console.SetCursorPosition(0, y);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new string(' ', Math.Max(0, w - 1)));
                Console.ResetColor();
                Delay(4);
            }
            SafeClear();
        }

        public static void ConfettiBurst(int rows = 8)
        {
            if (!Settings.EnableAnimations) return;

            int w;
            try { w = Math.Clamp(Console.WindowWidth, 60, 160); }
            catch { w = 80; }

            var colors = new[]
            {
                ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Yellow,
                ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.White
            };
            char[] bits = { '✦', '✺', '*', '+', '•', 'x', '◆' };
            var rng = new Random();

            for (int r = 0; r < rows; r++)
            {
                int count = w / 6;
                var sb = new System.Text.StringBuilder(w);
                for (int i = 0; i < count; i++)
                {
                    sb.Append(' ');
                    sb.Append(bits[rng.Next(bits.Length)]);
                    sb.Append(' ');
                }
                Console.ForegroundColor = colors[rng.Next(colors.Length)];
                Console.WriteLine(sb.ToString());
                Console.ResetColor();
                Delay(35);
            }
        }

        public static void TrophyReveal(string title)
        {
            if (!Settings.EnableAnimations || !Settings.EnableCinematicTransitions)
            {
                Console.WriteLine(title);
                return;
            }

            string[] trophy =
            {
                "        ___________        ",
                "       '._==_==_=_.'       ",
                "       .-\\:      /-.      ",
                "      | (|:.     |) |      ",
                "       '-|:.     |-'       ",
                "         \\::.    /        ",
                "          '::. .'          ",
                "            ) (            ",
                "          _.' '._          ",
                "         `\"\"\"\"\"\"\"`         "
            };

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var line in trophy)
            {
                Console.WriteLine(line);
                Delay(18);
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.ResetColor();
            Delay(120);
        }

        private static void Delay(int ms)
        {
            if (ms <= 0) return;
            try { Thread.Sleep(ms); } catch { }
        }
    }
}
