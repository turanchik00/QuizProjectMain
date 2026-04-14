namespace ProjectNew.Core.Models
{
    public sealed class AppSettings
    {
        public bool EnableAnimations { get; set; } = true;
        public bool EnableSound { get; set; } = true;

        public bool EnableCinematicTransitions { get; set; } = true;

        // Kiçik dəyər = daha sürətli animasiya. 0 = instant.
        public int AnimationDelayMs { get; set; } = 8;

        // Console-da çox təmiz görünüş üçün.
        public bool ClearBetweenQuestions { get; set; } = true;

        // UI tema adı: Neon / Matrix / Sunset / Ice (ThemeRegistry)
        public string Theme { get; set; } = "Neon";
    }
}
