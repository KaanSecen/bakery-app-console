using BakeryApp.Core;
using Spectre.Console;

namespace BakeryAppConsole
{
    static class Program
    {
        static void Main()
        {
            List<BreadType> breadTypesList = Enum.GetValues(typeof(BreadType)).Cast<BreadType>().ToList();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Sandwich> sandwiches = new List<Sandwich>();
            List<Sandwich> soldSandwiches = new List<Sandwich>();
            
            
            var bakeryName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the bakery name")
                    .DefaultValue("My Bakery")
                    .PromptStyle(new Style(foreground: Color.Yellow))
            );

            Bakery bakery = new Bakery(bakeryName, breadTypesList, ingredients, sandwiches, soldSandwiches);
            BakeryPrompt bakeryPrompt = new BakeryPrompt(breadTypesList, ingredients, sandwiches, bakery);
            bool running = true;

            bakery.LoadData();
            
            while (running)
            {
                DisplayMenu(bakery);

                var choices = new[] { "Create Sandwich", "Sell Sandwich", "Create Ingredient", "Display Total Revenue", "Save Data", "Load Data", "Change Bakery Name", "Show menu", "Exit" };

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
                    ProcessChoice(bakery, Array.IndexOf(choices, selection) + 1, bakeryPrompt);
                }
            }
        }

        private static void ProcessChoice(Bakery bakery, int choice, BakeryPrompt bakeryPrompt)
        {
            switch (choice)
            {
                case 1:
                    bakery.CreateSandwich();
                    break;
                case 2:
                    bakery.SellSandwich();
                    break;
                case 3:
                    bakery.CreateIngredient();
                    break;
                case 4:
                    bakery.ShowRevenue();
                    break;
                case 5:
                    bakery.SaveData();
                    break;
                case 6:
                    bakery.LoadData();
                    break;
                case 7:
                    bakery.ChangeBakeryName();
                    break;
                case 8:
                    bakery.ShowMenu();
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
