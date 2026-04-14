using ProjectNew.Core.Enums;
using ProjectNew.Services;
using ProjectNew.UI;
using ProjectNew.UI.Effects;
using ProjectNew.UI.Sound;
using ProjectNew.UI.Theming;
using System.Text;

namespace ProjectNew
{
    class Program
    {
        static bool isAdmin = false;
        private const string AdminPassword = "2006";

        static QuestionManager questionManager = new QuestionManager();
        static QuizEngine quizEngine = new QuizEngine(questionManager);
        static SettingsService settingsService = new SettingsService();

        private static DifficultyLevel selectedDifficulty = DifficultyLevel.Easy;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            settingsService.Load();
            ConsoleFx.Settings = settingsService.Current;
            SoundFx.Settings = settingsService.Current;
            ConsoleUi.Theme = ThemeRegistry.Get(settingsService.Current.Theme);

            bool isRunning = true;

            while (isRunning)
            {
                string choice = MenuDisplay.ShowMenuAndGetSelection(isAdmin, selectedDifficulty, settingsService.Current);
                ConsoleFx.SafeClear();

                switch (choice)
                {
                    case "1":
                        SoundFx.Click();
                        quizEngine.StartQuiz(selectedDifficulty);
                        break;
                    case "2":
                        SoundFx.Click();
                        quizEngine.StartMixedQuiz();
                        break;
                    case "3":
                        SoundFx.Click();
                        ChangeDifficulty();
                        break;
                    case "4":
                        SoundFx.Click();
                        quizEngine.ShowHistory();
                        break;
                    case "5":
                        SoundFx.Click();
                        quizEngine.ShowStatistics();
                        break;
                    case "6":
                        SoundFx.Click();
                        HandleAdminAccess();
                        break;
                    case "7":
                        if (isAdmin) questionManager.AddQuestion();
                        else MenuDisplay.UnauthorizedAccess();
                        break;
                    case "8":
                        if (isAdmin) questionManager.ViewQuestions();
                        else MenuDisplay.UnauthorizedAccess();
                        break;
                    case "9":
                        if (isAdmin) questionManager.DeleteQuestion();
                        else MenuDisplay.UnauthorizedAccess();
                        break;
                    case "S":
                    case "s":
                        SettingsScreen.Show(settingsService);
                        break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine("Proqramdan çıxılır. Sağ olun!");
                        break;
                    default:
                        SoundFx.Wrong();
                        Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nDavamı üçün bir düyməyə basın...");
                    Console.ReadKey(true);
                }
            }
        }

        static void ChangeDifficulty()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Çətinlik dərəcəsini seçin =====");
            Console.ResetColor();
            Console.WriteLine("[1] Easy\n[2] Medium\n[3] Hard");
            Console.Write("Seçiminiz: ");

            string diffKey = Console.ReadLine()?.Trim();
            selectedDifficulty = diffKey switch
            {
                "1" => DifficultyLevel.Easy,
                "2" => DifficultyLevel.Medium,
                "3" => DifficultyLevel.Hard,
                _ => selectedDifficulty
            };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSeçim yadda saxlanıldı: {selectedDifficulty}");
            Console.ResetColor();
        }

        static void HandleAdminAccess()
        {
            if (isAdmin)
            {
                isAdmin = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Admin panelindən çıxış edildi.");
                Console.ResetColor();
            }
            else
            {
                Console.Write("Admin şifrəsini daxil edin: ");
                string inputPass = InputHandler.GetMaskedPassword();

                if (inputPass == AdminPassword)
                {
                    isAdmin = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nGiriş uğurludur!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYanlış şifrə!");
                    Console.ResetColor();
                }
            }
        }
    }
}
