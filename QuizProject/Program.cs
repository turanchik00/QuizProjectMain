namespace QuizProject
{
    class Program
    {
        static void Main(string[] args)
        {
            QuizEngine engine = new QuizEngine();

            bool running = true;

            while (running)
            {
                MenuPrinter.PrintMainMenu();

                int choice = InputValidator.GetValidInteger("\nSelect an option: ", 0, 8);

                switch (choice)
                {
                    case 1:
                        engine.StartQuiz();
                        break;
                    case 4:
                        MenuPrinter.PrintMessage("Admin mode: Adding question...", ConsoleColor.Green);
                        break;
                    case 0:
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