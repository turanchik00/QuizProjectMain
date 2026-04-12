using QuizProject.Data;
using QuizProject.Interfaces;
using QuizProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace QuizProject.Service
{
    public class QuizEngine : IQuizService
    {
        private readonly JsonContext _db;

        public QuizEngine()
        {
            _db = new JsonContext();
        }

        public List<MultipleChoiceQuestion> ShuffleQuestions(List<MultipleChoiceQuestion> questions)
        {
            Random rng = new Random();
            return questions.OrderBy(a => rng.Next()).ToList();
        }

        public void Play(List<MultipleChoiceQuestion> questions, string username)
        {
            if (questions == null || questions.Count == 0)
            {
                Console.WriteLine("Oynamaq ucun sual tapilmadi!");
                return;
            }

            int correctCount = 0;
            int wrongCount = 0;
            var shuffledQuestions = ShuffleQuestions(questions);

            Console.Clear();
            Console.WriteLine($"--- Quiz Basladi! Ugurlar, {username}! ---\n");

            foreach (var q in shuffledQuestions)
            {
                Console.WriteLine($"Sual: {q.Text}");

                for (int i = 0; i < q.Options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {q.Options[i]}");
                }

                Console.Write("\nCavabinizi daxil edin (reqem): ");
                int userChoice;

                while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > q.Options.Count)
                {
                    Console.Write("Zehmet olmasa duzgun variantin reqemini yazin : ");
                }

                if (userChoice - 1 == q.CorrectOptionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tebrikler! Dogru cavab.\n");
                    Console.ResetColor();
                    correctCount++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Sehvdir! Dogru cavab: {q.Options[q.CorrectOptionIndex]}\n");
                    Console.ResetColor();
                    wrongCount++;
                }

                FinishQuiz(username, correctCount, wrongCount);
            }
        }
        private void FinishQuiz(string username, int correct, int wrong)
        {
            Console.WriteLine("============================");
            Console.WriteLine("        OYUN BİTDİ!         ");
            Console.WriteLine($"Düzgün: {correct}");
            Console.WriteLine($"Səhv: {wrong}");
            Console.WriteLine($"Ümumi Bal: {correct * 10}");
            Console.WriteLine("============================\n");

            Score finalScore = new Score
            {
                Username = username,
                CorrectAnswers = correct,
                WrongAnswers = wrong,
                TotalScore = correct * 10
            };

            var history = _db.LoadHistory();
            history.Add(finalScore);
            _db.SaveHistory(history);

            Console.WriteLine("Nəticəniz tarixçəyə əlavə olundu. Davam etmək üçün bir düyməyə sıxın...");
            Console.ReadKey();
        }
    }
}
