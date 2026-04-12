namespace QuizProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                MenuPrinter.PrintMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // QuizEngine.StartNormalQuiz();
                        MenuPrinter.PrintMessage("Quiz starting soon...", ConsoleColor.Yellow);
                        break;
                    case "4":
                        // QuestionManager.AddNewQuestion();
                        MenuPrinter.PrintMessage("Admin mode: Adding question...", ConsoleColor.Green);
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Exiting... Goodbye!");
                        break;
                    default:
                        MenuPrinter.PrintMessage("Invalid option! Try again.", ConsoleColor.Red);
                        break;
                }
            }
        }
    }
}
