namespace ProjectNew.UI.Theming
{
    public sealed class ConsoleTheme
    {
        public ConsoleColor Accent { get; set; } = ConsoleColor.Cyan;
        public ConsoleColor Accent2 { get; set; } = ConsoleColor.Magenta;
        public ConsoleColor Success { get; set; } = ConsoleColor.Green;
        public ConsoleColor Warning { get; set; } = ConsoleColor.Yellow;
        public ConsoleColor Error { get; set; } = ConsoleColor.Red;
        public ConsoleColor Muted { get; set; } = ConsoleColor.DarkGray;

        public static ConsoleTheme Neon() => new ConsoleTheme
        {
            Accent = ConsoleColor.Cyan,
            Accent2 = ConsoleColor.Magenta,
            Success = ConsoleColor.Green,
            Warning = ConsoleColor.Yellow,
            Error = ConsoleColor.Red,
            Muted = ConsoleColor.DarkGray
        };
    }
}

