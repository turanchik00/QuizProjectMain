using System;
using System.Collections.Generic;
using System.Text;

namespace QuizProject.Service
{
    public class QuizEngine : IQuizService
    {
        public void StartQuiz()
        {
            MenuPrinter.PrintHeader();
            var questions = GetSampleQuestions();
            int correctCount = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                Console.Clear();
                MenuPrinter.PrintHeader();
                Console.WriteLine($"Question {i + 1} of {questions.Count}");
                Console.WriteLine("-------------------------------");

                var q = questions[i];
                Console.WriteLine($"\n{q.Text}");

                for (int j = 0; j < q.Options.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {q.Options[j]}");
                }

                int userAnswer = InputValidator.GetValidInteger("\nYour answer: ", 1, q.Options.Length);

                if (userAnswer == q.CorrectAnswerIndex + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("✔ Correct!");
                    correctCount++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"✘ Wrong! Correct answer was: {q.Options[q.CorrectAnswerIndex]}");
                }

                Console.ResetColor();
                Console.WriteLine("\nPress any key for next question...");
                Console.ReadKey();
            }

            ShowResult(correctCount, questions.Count);
        }

        public void ShowResult(int correct, int total)
        {
            double percentage = (double)correct / total * 100;
            Console.Clear();
            MenuPrinter.PrintHeader();
            Console.WriteLine("       --- QUIZ OVER ---");
            Console.WriteLine($"\nTotal Questions: {total}");
            Console.WriteLine($"Correct Answers: {correct}");
            Console.WriteLine($"Success Rate: {percentage:F2}%");

            Console.ForegroundColor = percentage >= 50 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(percentage >= 50 ? "🎉 Well done!" : "😟 Keep practicing!");
            Console.ResetColor();

            MenuPrinter.PrintMessage("Returning to main menu...", ConsoleColor.Cyan);
        }

        private List<dynamic> GetSampleQuestions()
        {
            return new List<dynamic>
            {
                new { Text = "Which language is used for C# development?", Options = new[] { "Java", "C#", "Python", "Swift" }, CorrectAnswerIndex = 1 },
                new { Text = "What is 5 + 5?", Options = new[] { "55", "11", "10", "20" }, CorrectAnswerIndex = 2 },
                new { Text = "Who developed C#?", Options = new[] { "Google", "Apple", "Microsoft", "Meta" }, CorrectAnswerIndex = 2 }
            };
        }
    }
}
