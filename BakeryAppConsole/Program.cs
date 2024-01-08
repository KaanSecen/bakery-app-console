using BakeryApp.Core;
using Spectre.Console;

namespace BakeryAppConsole
{
    static class Program
    {
        static void Main()
        {
            List<BreadType> breadTypesList = Enum.GetValues(typeof(BreadType)).Cast<BreadType>().ToList();
            
            var bakeryName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the bakery name")
                    .DefaultValue("My Bakery")
                    .PromptStyle(new Style(foreground: Color.Yellow))
            );

            Bakery bakery = new Bakery(bakeryName, breadTypesList);

            bool running = true;

            while (running)
            {
                DisplayMenu(bakery);

                var choices = new[] { "Create Sandwich", "Sell Sandwich", "Create Ingredient", "Display Total Revenue", "Save Data", "Load Data", "Change Bakery Name", "Exit" };

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
                    bakery.CreateSandwich();
                    break;
                case 2:
                    // Implement logic for selling a sandwich
                    break;
                case 3:
                    // Implement logic for displaying total revenue
                    var ingredient = bakery.CreateIngredient();
                    bakery.Ingredients.Add(ingredient);
                    foreach (Ingredient i in bakery.Ingredients)
                    {
                        Console.WriteLine(i);
                    }
                    Console.ReadLine();
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
                case 7:
                    
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
