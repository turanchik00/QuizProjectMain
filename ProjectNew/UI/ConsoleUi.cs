using ProjectNew.UI.Theming;

namespace ProjectNew.UI
{
    public static class ConsoleUi
    {
        public static ConsoleTheme Theme { get; set; } = ConsoleTheme.Neon();

        public static int WidthSafe
        {
            get
            {
                try { return Math.Clamp(Console.WindowWidth, 60, 220); }
                catch { return 80; }
            }
        }

        public static void WriteCentered(string text, ConsoleColor? color = null)
        {
            int w = WidthSafe;
            string line = text ?? "";
            int pad = Math.Max(0, (w - line.Length) / 2);
            if (color.HasValue) Console.ForegroundColor = color.Value;
            Console.WriteLine(new string(' ', pad) + line);
            Console.ResetColor();
        }

        public static void Hr(ConsoleColor? color = null, char ch = '─')
        {
            int w = WidthSafe;
            if (color.HasValue) Console.ForegroundColor = color.Value;
            Console.WriteLine(new string(ch, w));
            Console.ResetColor();
        }

        public static void Box(IEnumerable<string> lines, ConsoleColor? borderColor = null, ConsoleColor? textColor = null)
        {
            int w = WidthSafe;
            int inner = w - 4;
            borderColor ??= Theme.Accent;
            textColor ??= Console.ForegroundColor;

            Console.ForegroundColor = borderColor.Value;
            Console.WriteLine($"┌{new string('─', w - 2)}┐");
            Console.ResetColor();

            foreach (var raw in lines)
            {
                string s = raw ?? "";
                if (s.Length > inner) s = s[..inner];
                string padded = s.PadRight(inner);
                Console.ForegroundColor = borderColor.Value;
                Console.Write("│ ");
                Console.ResetColor();
                Console.ForegroundColor = textColor.Value;
                Console.Write(padded);
                Console.ResetColor();
                Console.ForegroundColor = borderColor.Value;
                Console.WriteLine(" │");
                Console.ResetColor();
            }

            Console.ForegroundColor = borderColor.Value;
            Console.WriteLine($"└{new string('─', w - 2)}┘");
            Console.ResetColor();
        }

        public static string ProgressBar(int current, int total, int width = 22)
        {
            if (total <= 0) total = 1;
            current = Math.Clamp(current, 0, total);
            width = Math.Clamp(width, 10, 40);
            int filled = (int)Math.Round((current / (double)total) * width);
            return $"[{new string('█', filled)}{new string('░', width - filled)}]";
        }

        public static void BigBanner(string subtitle)
        {
            WriteCentered(" ██████╗ ██╗   ██╗██╗███████╗", Theme.Accent);
            WriteCentered("██╔═══██╗██║   ██║██║╚══███╔╝", Theme.Accent);
            WriteCentered("██║   ██║██║   ██║██║  ███╔╝ ", Theme.Accent);
            WriteCentered("██║▄▄ ██║██║   ██║██║ ███╔╝  ", Theme.Accent2);
            WriteCentered("╚██████╔╝╚██████╔╝██║███████╗", Theme.Accent2);
            WriteCentered(" ╚══▀▀═╝  ╚═════╝ ╚═╝╚══════╝", Theme.Accent2);
            WriteCentered(subtitle, Theme.Muted);
        }
    }
}

