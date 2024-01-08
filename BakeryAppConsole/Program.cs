using BakeryApp.Core;
using Spectre.Console;

namespace BakeryAppConsole
{
    static class Program
    {
        static void Main()
        {
            var bakeryName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the bakery name")
                    .DefaultValue("My Bakery")
                    .PromptStyle(new Style(foreground: Color.Yellow))
            );

            Bakery bakery = new Bakery(bakeryName);

            bool running = true;

            while (running)
            {
                DisplayMenu(bakery);

                var choices = new[] { "Create Sandwich", "Sell Sandwich", "Display Total Revenue", "Save Data", "Load Data", "Change Bakery Name", "Exit" };

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Welcome to [yellow]{bakery.Name}[/] Bakery")
                        .PageSize(10)
                        .AddChoices(choices)
                        .HighlightStyle(new Style(foreground: Color.Yellow))
                );

                if (selection == "Exit")
                {
                    running = false;
                }
                else
                {
                    ProcessChoice(bakery, Array.IndexOf(choices, selection) + 1);
                }
            }
        }

        private static void ProcessChoice(Bakery bakery, int choice)
        {
            switch (choice)
            {
                case 1:
                    // Implement logic for creating a sandwich
                    break;
                case 2:
                    // Implement logic for selling a sandwich
                    break;
                case 3:
                    // Implement logic for displaying total revenue
                    break;
                case 4:
                    // Implement logic for saving data
                    break;
                case 5:
                    // Implement logic for loading data
                    break;
                case 6:
                    bakery.ChangeBakeryName();
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        public static void DisplayMenu(Bakery bakery)
        {
            AnsiConsole.Clear();
        }
    }
}
