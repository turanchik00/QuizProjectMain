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

                int choice = InputValidator.GetValidInteger("\nSelect an option: ", 0, 8);

                switch (choice)
                {
                    case 1: 
                        MenuPrinter.PrintMessage("Quiz starting soon...", ConsoleColor.Yellow);
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