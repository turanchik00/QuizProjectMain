using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.IO;

namespace ProjectNew.Data
{
    public static class JsonContext
    {
        private static string ResolvePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return filePath;
            if (Path.IsPathRooted(filePath)) return filePath;
            return Path.Combine(AppContext.BaseDirectory, filePath);
        }

        public static List<T> Read<T>(string filePath)
        {
            string path = ResolvePath(filePath);
            if (!File.Exists(path)) return new List<T>();
            string json = File.ReadAllText(path, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void Write<T>(string filePath, List<T> data)
        {
            string path = ResolvePath(filePath);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json, Encoding.UTF8);
        }
    }
}
