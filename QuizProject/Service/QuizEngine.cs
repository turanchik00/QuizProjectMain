using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Service
{
    public class QuizEngine : IQuizService
    {
        // 1. Oyunu başladan ana metod
        public void StartQuiz()
        {
            Console.Clear();
            MenuPrinter.PrintHeader();

            // Sualları gətiririk
            var questions = GetSampleQuestions();

            // Sənin yazdığın Extension-dan istifadə edərək sualları qarışdırırıq
            questions.Shuffle();

            // Maksimum 10 sual göstərsin (və ya siyahıda nə qədər varsa)
            int limit = Math.Min(10, questions.Count);
            var activeQuestions = questions.Take(limit).ToList();

            int correctAnswers = 0;

            for (int i = 0; i < activeQuestions.Count; i++)
            {
                var q = activeQuestions[i];
                bool isCorrect = AskQuestion(q, i + 1, activeQuestions.Count);

                if (isCorrect) correctAnswers++;
            }

            // Nəticəni göstəririk
            ShowResult(correctAnswers, activeQuestions.Count);
        }

        // 2. Tək bir sualı ekrana çıxaran və cavabı yoxlayan köməkçi metod
        private bool AskQuestion(dynamic q, int currentNum, int total)
        {
            Console.Clear();
            MenuPrinter.PrintHeader();
            Console.WriteLine($"Question {currentNum} of {total}");
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"\n{q.Text}");

            for (int i = 0; i < q.Options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {q.Options[i]}");
            }

            // Validator vasitəsilə düzgün seçim alırıq
            int choice = InputValidator.GetValidInteger("\nYour answer: ", 1, q.Options.Length);

            if (choice == q.CorrectAnswerIndex + 1)
            {
                "✔ Correct!".PrintWithColor(ConsoleColor.Green);
                Console.WriteLine("\nPress any key for next...");
                Console.ReadKey();
                return true;
            }
            else
            {
                $"✘ Wrong! Correct: {q.Options[q.CorrectAnswerIndex]}".PrintWithColor(ConsoleColor.Red);
                Console.WriteLine("\nPress any key for next...");
                Console.ReadKey();
                return false;
            }
        }

        // 3. Oyun sonu statistikasını göstərən metod (Interface-dən gəlir)
        public void ShowResult(int correct, int total)
        {
            double percentage = (double)correct / total * 100;

            Console.Clear();
            MenuPrinter.PrintHeader();
            "--- QUIZ COMPLETED ---".PrintWithColor(ConsoleColor.Cyan);

            Console.WriteLine($"\nTotal Questions: {total}");
            Console.WriteLine($"Correct Answers: {correct}");
            Console.WriteLine($"Success Rate: {percentage:F1}%");

            Console.WriteLine("\n-------------------------------");
            if (percentage >= 80) "🏆 EXCELLENT!".PrintWithColor(ConsoleColor.Yellow);
            else if (percentage >= 50) "👍 PASS!".PrintWithColor(ConsoleColor.Green);
            else "📚 NEED MORE STUDY!".PrintWithColor(ConsoleColor.Red);
            Console.WriteLine("-------------------------------\n");

            MenuPrinter.PrintMessage("Returning to menu...", ConsoleColor.DarkGray);
            Console.ReadKey();
        }

        // Müvəqqəti test sualları (Turan JSON-u bitirənə qədər)
        private List<dynamic> GetSampleQuestions()
        {
            return new List<dynamic>
            {
                new { Text = "What is C#?", Options = new[] { "Language", "Fruit", "Car", "City" }, CorrectAnswerIndex = 0 },
                new { Text = "Which keyword is used for constants?", Options = new[] { "var", "let", "const", "static" }, CorrectAnswerIndex = 2 },
                new { Text = "Visual Studio is an...?", Options = new[] { "OS", "IDE", "Browser", "Database" }, CorrectAnswerIndex = 1 },
                new { Text = "Who created C#?", Options = new[] { "Anders Hejlsberg", "Bill Gates", "Elon Musk", "Steve Jobs" }, CorrectAnswerIndex = 0 },
                new { Text = "Is C# object-oriented?", Options = new[] { "Yes", "No", "Maybe", "Only on Sundays" }, CorrectAnswerIndex = 0 }
            };
        }
    }
}
