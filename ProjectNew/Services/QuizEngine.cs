using ProjectNew.Core.Enums;
using ProjectNew.Core.Exceptions;
using ProjectNew.Core.Interfaces;
using ProjectNew.Core.Models;
using ProjectNew.UI;
using ProjectNew.UI.Effects;
using ProjectNew.UI.Sound;
using ProjectNew.UI.Theming;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ProjectNew.Services
{
    public class QuizEngine : IQuizService
    {
        private readonly IQuestionService _qService;
        private const string HistoryPath = "history.json";
        private static string ResolvedHistoryPath => Path.Combine(AppContext.BaseDirectory, HistoryPath);

        public QuizEngine(IQuestionService qService)
        {
            _qService = qService;
        }

        private void SaveResultToHistory(string user, int c, int w, string diff)
        {
            List<QuizResult> history = new List<QuizResult>();
            if (File.Exists(ResolvedHistoryPath))
                history = JsonSerializer.Deserialize<List<QuizResult>>(File.ReadAllText(ResolvedHistoryPath)) ?? new List<QuizResult>();

            history.Add(new QuizResult { Username = user, CorrectCount = c, WrongCount = w, Difficulty = diff, Date = DateTime.Now });
            File.WriteAllText(ResolvedHistoryPath, JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void StartMixedQuiz()
        {
            var all = _qService.GetAllQuestions();
            if (!all.Any())
            {
                ConsoleFx.SafeClear();
                ConsoleUi.Box(new[] { "Sual tapılmadı. `Questions.json` boşdur." }, ConsoleUi.Theme.Error, ConsoleUi.Theme.Muted);
                return;
            }

            Random rng = new Random();
            // 3 difficulty-dən qarışıq: hər birindən maksimum 5 (mövcud olan qədər)
            var picked = all
                .GroupBy(q => q.Difficulty)
                .OrderBy(g => rng.Next())
                .SelectMany(g => g.OrderBy(_ => rng.Next()).Take(5))
                .OrderBy(_ => rng.Next())
                .ToList();

            RunQuiz(picked, "Mixed");
        }

        public void StartQuiz(DifficultyLevel selectedLevel)
        {
            var allQuestions = _qService.GetAllQuestions();
            var filteredQuestions = allQuestions.Where(q => q.Difficulty == selectedLevel).ToList();

            if (!filteredQuestions.Any())
            {
                Console.WriteLine($"\nTəəssüf ki, {selectedLevel} səviyyəsində sual tapılmadı.");
                return;
            }

            Random rng = new Random();
            var quizQuestions = filteredQuestions.OrderBy(a => rng.Next()).ToList();
            RunQuiz(quizQuestions, selectedLevel.ToString());
        }

        private void RunQuiz(List<Question> quizQuestions, string modeLabel)
        {
            ConsoleFx.SafeClear();
            Console.InputEncoding = Encoding.UTF8;

            string username = "";
            while (username.Length < 3 || username.Length > 15)
            {
                ConsoleUi.Box(new[]
                {
                    "İstifadəçi adı daxil edin",
                    "Qaydalar: 3-15 simvol",
                    "",
                    "Tip: adınızı yazıb Enter basın"
                }, ConsoleUi.Theme.Accent2, ConsoleUi.Theme.Muted);

                Console.ForegroundColor = ConsoleUi.Theme.Accent;
                Console.Write("Username: ");
                Console.ResetColor();
                username = Console.ReadLine()?.Trim() ?? "";
                if (username.Length < 3 || username.Length > 15)
                {
                    SoundFx.Wrong();
                    ConsoleFx.ErrorShake("Xəta: Adın uzunluğu uyğun deyil!");
                    ConsoleFx.SafeClear();
                }
            }

            int correct = 0, wrong = 0, streak = 0, bestStreak = 0;

            SoundFx.Start();
            ConsoleFx.Countdown(modeLabel, 3);

            for (int i = 0; i < quizQuestions.Count; i++)
            {
                var q = quizQuestions[i];
                if (ConsoleFx.Settings.ClearBetweenQuestions) ConsoleFx.ScreenWipe();

                ConsoleUi.BigBanner($"Mode: {modeLabel}  •  Player: {username}");
                Console.WriteLine();
                ConsoleUi.Box(new[]
                {
                    $"Sual {i + 1}/{quizQuestions.Count}  {ConsoleUi.ProgressBar(i, quizQuestions.Count)}",
                    $"Xal: {correct} düz / {wrong} səhv    Streak: {streak} (Ən yaxşı {bestStreak})",
                    $"Çətinlik: {q.Difficulty}"
                }, ConsoleUi.Theme.Accent, ConsoleUi.Theme.Muted);

                Console.WriteLine();
                ConsoleUi.Box(new[]
                {
                    q.Text
                }, ConsoleUi.Theme.Accent2, ConsoleColor.White);

                char[] optionLetters = { 'A', 'B', 'C', 'D' };
                var optionLines = new List<string>();
                for (int j = 0; j < q.Options.Count; j++)
                    optionLines.Add($"{optionLetters[j]}) {q.Options[j]}");

                Console.WriteLine();
                ConsoleUi.Box(optionLines, ConsoleUi.Theme.Muted, ConsoleColor.White);

                int answerIndex = -1;
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleUi.Theme.Warning;
                        Console.Write("Cavabınız (A/B/C/D): ");
                        Console.ResetColor();

                        string inputStr = InputHandler.GetValidVariant();
                        char choiceChar = inputStr[0];

                        answerIndex = choiceChar switch
                        {
                            'A' => 0,
                            'B' => 1,
                            'C' => 2,
                            'D' => 3,
                            _ => throw new InvalidVariantException("Xəta: Keçərsiz variant!")
                        };
                        break;
                    }
                    catch (InvalidVariantException ex)
                    {
                        SoundFx.Wrong();
                        ConsoleFx.ErrorShake(ex.Message);
                    }
                }

                bool isCorrect = answerIndex == q.CorrectOptionIndex;
                if (isCorrect)
                {
                    correct++;
                    streak++;
                    bestStreak = Math.Max(bestStreak, streak);
                    SoundFx.Correct();
                    ConsoleFx.SuccessFlash("Düz cavab!");
                }
                else
                {
                    wrong++;
                    streak = 0;
                    SoundFx.Wrong();
                    char correctLetter = optionLetters[Math.Clamp(q.CorrectOptionIndex, 0, optionLetters.Length - 1)];
                    ConsoleFx.ErrorShake($"Səhv cavab! Doğru: {correctLetter}");
                }

                Console.ForegroundColor = ConsoleUi.Theme.Muted;
                Console.WriteLine("Davam etmək üçün bir düymə...");
                Console.ResetColor();
                Console.ReadKey(true);
            }

            ConsoleFx.SafeClear();
            SoundFx.Finish();

            ConsoleUi.BigBanner("Results");
            Console.WriteLine();
            int total = correct + wrong;
            int acc = total == 0 ? 0 : (int)Math.Round(correct * 100.0 / total);
            if (acc >= 80)
            {
                ConsoleFx.TrophyReveal($"Mükəmməl! Accuracy {acc}%");
                ConsoleFx.ConfettiBurst(10);
            }
            ConsoleUi.Box(new[]
            {
                $"Player: {username}",
                $"Mode:   {modeLabel}",
                "",
                $"Düz: {correct}",
                $"Səhv:   {wrong}",
                $"Ən yaxşı nəticə: {bestStreak}",
                "",
                $"Accuracy: {acc}%"
            }, ConsoleUi.Theme.Success, ConsoleColor.White);

            SaveResultToHistory(username, correct, wrong, modeLabel);
        }

        public void ShowHistory()
        {
            ConsoleFx.SafeClear();
            ConsoleFx.LoadingDots("Tarixçə yüklənir", 3);
            if (!File.Exists(ResolvedHistoryPath)) { Console.WriteLine("Tarixçə tapılmadı."); return; }
            var history = JsonSerializer.Deserialize<List<QuizResult>>(File.ReadAllText(ResolvedHistoryPath));

            ConsoleUi.BigBanner("History");
            Console.WriteLine();
            ConsoleUi.Hr(ConsoleUi.Theme.Muted);

            foreach (var item in history ?? new List<QuizResult>())
            {
                ConsoleUi.Box(new[]
                {
                    $"User: {item.Username}",
                    $"Mode/Çətinlik: {item.Difficulty}",
                    $"Xal: {item.CorrectCount} correct / {item.WrongCount} wrong",
                    $"Tarix: {item.Date:g}"
                }, ConsoleUi.Theme.Accent2, ConsoleColor.White);
                Console.WriteLine();
            }
        }

        public void ShowStatistics()
        {
            ConsoleFx.SafeClear();
            ConsoleFx.LoadingDots("Statistikalar hazırlanır", 3);
            if (!File.Exists(ResolvedHistoryPath)) { Console.WriteLine("Məlumat yoxdur."); return; }
            var history = JsonSerializer.Deserialize<List<QuizResult>>(File.ReadAllText(ResolvedHistoryPath)) ?? new List<QuizResult>();

            var topUsers = history
                .GroupBy(h => h.Username)
                .Select(g => new { User = g.Key, MaxScore = g.Max(x => x.CorrectCount) })
                .OrderByDescending(u => u.MaxScore)
                .Take(3);

            ConsoleUi.BigBanner("Scoreboard");
            int rank = 1;
            var lines = new List<string>();
            foreach (var u in topUsers)
            {
                lines.Add($"{rank}. {u.User}  —  best: {u.MaxScore} correct");
                rank++;
            }
            if (!lines.Any()) lines.Add("Hələ nəticə yoxdur.");
            Console.WriteLine();
            ConsoleUi.Box(lines, ConsoleUi.Theme.Warning, ConsoleColor.White);

            double totalCorrect = history.Sum(h => h.CorrectCount);
            double totalQuestions = history.Sum(h => h.CorrectCount + h.WrongCount);

            Console.WriteLine();
            ConsoleUi.Box(new[]
            {
                $"Ümumi quiz sayı: {history.Count}",
                $"Ümumi düzgün cavablar : {(int)totalCorrect}",
                $"Ümumi cavablar: {(int)totalQuestions}",
            }, ConsoleUi.Theme.Accent, ConsoleColor.White);
        }
    }
}
