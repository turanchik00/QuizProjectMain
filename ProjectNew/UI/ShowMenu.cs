using ProjectNew.Core.Enums;
using ProjectNew.Core.Models;
using ProjectNew.UI.Effects;
using ProjectNew.UI.Menu;

namespace ProjectNew.UI
{
    public static class MenuDisplay
    {
        public static string ShowMenuAndGetSelection(bool isAdmin, DifficultyLevel difficulty, AppSettings settings)
        {
            var header = new[]
            {
                $"Vəziyyət  •  Çətinlik: {difficulty}   Admin: {(isAdmin ? "ON" : "OFF")}",
            };

            var items = new List<MenuItem>
            {
                new MenuItem { Id = "1", Label = "Quizə Başla (seçilmiş çətinlik ilə)" },
                new MenuItem { Id = "2", Label = "Qarışıq Quiz (qarışdırılmış suallar))" },
                new MenuItem { Id = "3", Label = "Çətinlik Dərəcəsi" },
                new MenuItem { Id = "4", Label = "Quiz Tarixçəsi" },
                new MenuItem { Id = "5", Label = "Statistikalar" },
                new MenuItem { Id = "S", Label = "Ayarlar" },
                new MenuItem { Id = "6", Label = isAdmin ? "Logout from Admin" : "Admin Panel Login" },
                new MenuItem { Id = "7", Label = "Sual Əlavə Et (Admin)", Enabled = isAdmin },
                new MenuItem { Id = "8", Label = "Bütün Suallar (Admin)", Enabled = isAdmin },
                new MenuItem { Id = "9", Label = "Sual Sil (Admin)", Enabled = isAdmin },
                new MenuItem { Id = "0", Label = "Çıxış" }
            };

            return MenuNavigator.Show(
                title: "Quiz Manager • Nicat • Turan",
                headerLines: header,
                items: items,
                footerHint: "UP/DOWN ilə seç • ENTER ilə aç • ESC = çıxış • Hotkeys: 1-9, S, 0"
            );
        }

        public static void UnauthorizedAccess()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Xəta: Bu bölməyə giriş icazəniz yoxdur!");
            Console.ResetColor();
        }
    }
}
