using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Helpers
{
    namespace QuizProjectMain.Helpers
    {
        public static class Extensions
        {
            private static Random _rng = new Random();

            // 1. Siyahını qarışdıran metod (Shuffle)
            // Artıq hər yerdə list.Shuffle() yazıb istifadə edə bilərsən
            public static void Shuffle<T>(this IList<T> list)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = _rng.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }

            // 2. Yazını müəyyən uzunluğa qədər qısaldan metod (Məs: Sual çox uzundursa ...)
            public static string Truncate(this string value, int maxLength)
            {
                if (string.IsNullOrEmpty(value)) return value;
                return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
            }

            // 3. Rəngli yazı çıxarmağı daha da qısaldan metod
            public static void PrintWithColor(this string text, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(text);
                Console.ResetColor();
            }
        }
    }
