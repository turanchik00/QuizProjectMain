using ProjectNew.UI.Effects;
using ProjectNew.UI.Sound;

namespace ProjectNew.UI.Menu
{
    public static class MenuNavigator
    {
        public static string Show(string title, IEnumerable<string> headerLines, IList<MenuItem> items, string footerHint)
        {
            if (items.Count == 0) return "";

            int idx = 0;
            idx = NextEnabled(items, idx, +1);

            while (true)
            {
                Render(title, headerLines, items, idx, footerHint);

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    int next = NextEnabled(items, idx - 1, -1);
                    if (next != idx) { idx = next; SoundFx.Click(); }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    int next = NextEnabled(items, idx + 1, +1);
                    if (next != idx) { idx = next; SoundFx.Click(); }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    SoundFx.Click();
                    return items[idx].Id;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    SoundFx.Wrong();
                    return "0";
                }
                else
                {
                    // Quick hotkeys: 0-9 / S
                    char ch = char.ToUpperInvariant(key.KeyChar);
                    string? direct = items.FirstOrDefault(i => i.Enabled && i.Id.Length == 1 && char.ToUpperInvariant(i.Id[0]) == ch)?.Id;
                    if (!string.IsNullOrWhiteSpace(direct))
                    {
                        SoundFx.Click();
                        return direct;
                    }
                }
            }
        }

        private static void Render(string title, IEnumerable<string> headerLines, IList<MenuItem> items, int selected, string footerHint)
        {
            ConsoleFx.SafeClear();
            ConsoleUi.BigBanner(title);
            Console.WriteLine();

            ConsoleUi.Box(headerLines, ConsoleUi.Theme.Accent, ConsoleUi.Theme.Muted);
            Console.WriteLine();

            var lines = new List<string>();
            for (int i = 0; i < items.Count; i++)
            {
                var it = items[i];
                bool isSel = i == selected;
                string prefix = isSel ? "> " : "  ";
                string label = it.Enabled ? it.Label : $"{it.Label} (kilidli)";
                lines.Add(prefix + label);
            }

            ConsoleUi.Box(lines, ConsoleUi.Theme.Accent2, ConsoleColor.White);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleUi.Theme.Muted;
            Console.WriteLine(footerHint);
            Console.ResetColor();
        }

        private static int NextEnabled(IList<MenuItem> items, int start, int step)
        {
            if (items.Count == 0) return 0;

            int idx = start;
            if (idx < 0) idx = items.Count - 1;
            if (idx >= items.Count) idx = 0;

            for (int tries = 0; tries < items.Count; tries++)
            {
                if (items[idx].Enabled) return idx;
                idx += step;
                if (idx < 0) idx = items.Count - 1;
                if (idx >= items.Count) idx = 0;
            }
            return 0;
        }
    }
}

