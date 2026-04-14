using ProjectNew.Core.Enums;
using ProjectNew.Core.Exceptions;
using ProjectNew.Core.Interfaces;
using ProjectNew.Core.Models;
using ProjectNew.Data;
using ProjectNew.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ProjectNew.Services
{
    public class QuestionManager : IQuestionService
    {
        private const string FilePath = "Questions.json";
        private List<Question> _questions;

        public QuestionManager()
        {
            _questions = JsonContext.Read<Question>(FilePath);
        }

        public List<Question> GetAllQuestions() => _questions;

        private void LoadQuestions()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                _questions = JsonSerializer.Deserialize<List<Question>>(json) ?? new List<Question>();
            }
        }

        private void SaveQuestions()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(_questions, options);
            File.WriteAllText(FilePath, json, Encoding.UTF8);
        }

        // --- Digər Admin Metodları ---
        public void AddQuestion()
        {
            Console.Clear();

            Question newQuestion = new Question { Id = _questions.Any() ? _questions.Max(q => q.Id) + 1 : 1 };

            Console.Write("Sual: ");
            newQuestion.Text = Console.ReadLine();

            for (int i = 0; i < 4; i++)
            {
                Console.Write($"{(char)('A' + i)}) ");
                newQuestion.Options.Add(Console.ReadLine());
            }

            int correctIndex = -1;
            while (correctIndex == -1)
            {
                try
                {
                    Console.Write("Düzgün variant (A/B/C/D): ");
                    string inp = InputHandler.GetValidVariant();
                    correctIndex = inp[0] switch { 'A' => 0, 'B' => 1, 'C' => 2, 'D' => 3, _ => -1 };
                }
                catch (InvalidVariantException ex) { Console.WriteLine(ex.Message); }
            }

            Console.WriteLine("Çətinlik dərəcəsini seçin: [1] Easy, [2] Medium, [3] Hard");
            string levelChoice = Console.ReadLine();
            newQuestion.Difficulty = levelChoice switch
            {
                "1" => DifficultyLevel.Easy,
                "2" => DifficultyLevel.Medium,
                "3" => DifficultyLevel.Hard,
                _ => DifficultyLevel.Easy
            };

            newQuestion.CorrectOptionIndex = correctIndex;
            _questions.Add(newQuestion);
            SaveQuestions();
        }

        // --- [8] Bütün Suallara Bax ---
        public void ViewQuestions()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("===== BÜTÜN SUALLAR (KATEQORİYALAR ÜZRƏ) =====\n");
            Console.ResetColor();

            if (!_questions.Any())
            {
                Console.WriteLine("Sistemdə heç bir sual yoxdur.");
                return;
            }

            // Sualları Çətinlik dərəcəsinə (Difficulty) görə qruplaşdırırıq və sıralayırıq
            var groupedQuestions = _questions.GroupBy(q => q.Difficulty).OrderBy(g => g.Key);
            char[] optionLetters = { 'A', 'B', 'C', 'D' };

            foreach (var group in groupedQuestions)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n[--- {group.Key} Səviyyəsi ---]");
                Console.ResetColor();

                foreach (var q in group)
                {
                    Console.WriteLine($"{q.Id}. {q.Text}");
                    for (int i = 0; i < q.Options.Count; i++)
                    {
                        string isCorrect = (i == q.CorrectOptionIndex) ? " (Doğru cavab)" : "";

                        if (i == q.CorrectOptionIndex) Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{optionLetters[i]}. {q.Options[i]}{isCorrect}");
                        Console.ResetColor();
                    }
                    Console.WriteLine("-------------------------");
                }
            }
        }

        // --- [9] Sual Sil ---
        public void DeleteQuestion()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            ViewQuestions();

            if (!_questions.Any()) return;

            Console.Write("\nSilmək istədiyiniz sualın ID-sini daxil edin (Ləğv etmək üçün hər hansı hərf yazın): ");
            if (int.TryParse(Console.ReadLine(), out int idToDelete))
            {
                var questionToRemove = _questions.FirstOrDefault(q => q.Id == idToDelete);

                if (questionToRemove != null)
                {
                    _questions.Remove(questionToRemove);
                    SaveQuestions();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nID: {idToDelete} olan sual uğurla silindi.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nXəta: {idToDelete} nömrəli ID tapılmadı.");
                    Console.ResetColor();
                }
            }
        }

        public List<Question> GetQuestionsByDifficulty(DifficultyLevel level)
        {
             return _questions.Where(q => q.Difficulty == level).ToList();  
        }
    }
}