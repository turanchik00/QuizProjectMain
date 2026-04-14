using ProjectNew.Core.Models;
using System.Text.Json;

namespace ProjectNew.Services
{
    public sealed class SettingsService
    {
        private const string FileName = "settings.json";

        public AppSettings Current { get; private set; } = new AppSettings();

        private static string ResolvePath(string fileName)
        {
            return Path.Combine(AppContext.BaseDirectory, fileName);
        }

        public void Load()
        {
            string path = ResolvePath(FileName);
            if (!File.Exists(path))
            {
                Save(); // default settings yarat
                return;
            }

            try
            {
                string json = File.ReadAllText(path);
                Current = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch
            {
                Current = new AppSettings();
                Save();
            }
        }

        public void Save()
        {
            string path = ResolvePath(FileName);
            var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }
    }
}
