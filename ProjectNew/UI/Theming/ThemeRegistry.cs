namespace ProjectNew.UI.Theming
{
    public static class ThemeRegistry
    {
        public static string[] Names { get; } = { "Neon", "Green", "Sunset", "Ice" };

        public static ConsoleTheme Get(string? name)
        {
            return (name ?? "").Trim().ToLowerInvariant() switch
            {
                "matrix" => new ConsoleTheme
                {
                    Accent = ConsoleColor.Green,
                    Accent2 = ConsoleColor.DarkGreen,
                    Success = ConsoleColor.Green,
                    Warning = ConsoleColor.Yellow,
                    Error = ConsoleColor.Red,
                    Muted = ConsoleColor.DarkGray
                },
                "sunset" => new ConsoleTheme
                {
                    Accent = ConsoleColor.Yellow,
                    Accent2 = ConsoleColor.Magenta,
                    Success = ConsoleColor.Green,
                    Warning = ConsoleColor.DarkYellow,
                    Error = ConsoleColor.Red,
                    Muted = ConsoleColor.DarkGray
                },
                "ice" => new ConsoleTheme
                {
                    Accent = ConsoleColor.Cyan,
                    Accent2 = ConsoleColor.Blue,
                    Success = ConsoleColor.Green,
                    Warning = ConsoleColor.Yellow,
                    Error = ConsoleColor.Red,
                    Muted = ConsoleColor.DarkGray
                },
                _ => ConsoleTheme.Neon()
            };
        }
    }
}

