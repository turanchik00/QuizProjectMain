using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Helpers
{
    public static class MenuPrinter
    {
        public static void PrintHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===========================================");
            Console.WriteLine("       🚀 QUIZ MASTER PRO v1.0 🚀        ");
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }

        public static void PrintMainMenu()
        {
            PrintHeader();
            Console.WriteLine("\n[1] Start Quiz");
            Console.WriteLine("[2] Random Quiz");
            Console.WriteLine("[3] Choose Difficulty");
            Console.WriteLine("-------------------------");
            Console.WriteLine("[4] Add Question (Admin)");
            Console.WriteLine("[5] View Questions");
            Console.WriteLine("[6] Delete Question");
            Console.WriteLine("-------------------------");
            Console.WriteLine("[7] Quiz History");
            Console.WriteLine("[8] Statistics");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[0] Exit");
            Console.ResetColor();
            Console.Write("\nSelect an option: ");
        }

        public static void PrintMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n>> {message}");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
