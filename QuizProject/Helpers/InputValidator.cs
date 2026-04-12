using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Helpers
{
    public static class InputValidator
    {
        public static int GetValidInteger(string message, int min, int max)
        {
            int input;
            while (true)
            {
                Console.Write(message);
                string rawInput = Console.ReadLine();

                if (int.TryParse(rawInput, out input) && input >= min && input <= max)
                {
                    return input;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[!] Error: Please enter a number between {min} and {max}.");
                Console.ResetColor();
            }
        }

        public static string GetValidString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[!] Error: This field cannot be empty!");
                Console.ResetColor();
            }
        }
    }
}
