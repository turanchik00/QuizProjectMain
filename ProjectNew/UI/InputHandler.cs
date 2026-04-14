using ProjectNew.Core.Enums;
using ProjectNew.Core.Exceptions;
using ProjectNew.Core.Models;
using ProjectNew.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectNew.UI
{
    public static class InputHandler
    {
        public static string GetValidVariant()
        {
            string input = Console.ReadLine()?.Trim().ToUpper();

            // Yalnız A, B, C, D variantlarını qəbul et
            if (input != "A" && input != "B" && input != "C" && input != "D")
            {
                throw new InvalidVariantException("Xəta: Yalnız A, B, C və ya D variantlarından birini daxil edə bilərsiniz!");
            }

            return input;
        }

        // Şifrə daxil edərkən ekranda * göstərən metod
        public static string GetMaskedPassword()
        {
            StringBuilder password = new StringBuilder();

            while (true)
            {
                // Ekranı dərhal çap etməməsi üçün 'true' parametrini göndəririk
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                // Əgər Enter basılarsa, şifrənin daxil edilməsi bitir
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                // Əgər Backspace (silmək) basılarsa
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.Length--; // StringBuilder-dən sonuncu simvolu sil
                        Console.Write("\b \b"); // Ekranda da sonuncu ulduzu sil
                    }
                }
                // Normal simvol daxil edilərsə (Ctrl, Alt kimi xüsusi düymələr xaric)
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    password.Append(keyInfo.KeyChar); // Şifrəyə əlavə et
                    Console.Write("*"); // Ekrana ulduz çap et
                }
            }

            return password.ToString();
        }
    }
}






































