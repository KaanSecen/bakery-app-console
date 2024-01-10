using Newtonsoft.Json;
using Spectre.Console;
namespace BakeryApp.Core;

public class Bakery
{
    public string Name { get; private set; }
    public List<BreadType> BreadTypesList { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Sandwich> Sandwiches { get; private set; }
    public List<Sandwich> SoldSandwiches { get; private set; }
    public double totalRevenue { get; set; }

    public Bakery(string name, List<BreadType> breadTypesList, List<Ingredient> ingredients, List<Sandwich> sandwiches, List<Sandwich> soldSandwiches)
    {
        Name = name;
        BreadTypesList = breadTypesList;
        Ingredients = ingredients;
        Sandwiches = sandwiches;
        SoldSandwiches = sandwiches;
    }

    public void ChangeBakeryName()
    {
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the new bakery name")
                .DefaultValue(Name)
                .PromptStyle(new Style(foreground: Color.Yellow))
        );

        Name = newName;
        AnsiConsole.MarkupLine($"Bakery name changed to: [yellow]{Name}[/]");
    }

    public void CreateSandwich(Sandwich sandwich)
    {
        if (Sandwiches.Any(s => s.Name == sandwich.Name))
        {
            throw new InvalidOperationException("Hey this sandwich already exists");
        }
    }
    
    public void CreateIngredient(Ingredient ingredient)
    {
        if (Ingredients.Any(i => i.Name == ingredient.Name))
        {
            throw new InvalidOperationException("Hey this ingredient already exists");
        }
    }

    public void ShowIngredients()
    {
        foreach (Ingredient ingredient in Ingredients)
        {
            Console.WriteLine(ingredient.ToString());
        }

        Console.ReadLine();
    }

    public double CalculatePrice(List<Sandwich> sandwiches)
    {
        return sandwiches.Sum(sandwich => sandwich.GetPrice());
    }

    public void ShowMenu()
    {
        var table = new Table().Centered();

        AnsiConsole.Live(table)
            .Start(ctx => 
            {
                table.AddColumn("Name");
                ctx.Refresh();

                table.AddColumn("Price");
                ctx.Refresh();

                foreach(Sandwich sandwich in Sandwiches)
                {
                    table.AddRow(sandwich.Name, sandwich.GetPrice().ToString() + " euro");
                    ctx.Refresh();
                    Thread.Sleep(50);
                }
            });
        Console.ReadLine();
    }

    public void SaveData()
    {
        AnsiConsole.Status()
            .Start("Saving...", ctx => 
            {
                // Simulate some work
                ctx.Spinner(Spinner.Known.Star);
                AnsiConsole.MarkupLine("Saving ingredients...");
                var jsonIngredients = JsonConvert.SerializeObject(Ingredients, Formatting.Indented);
                File.WriteAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Ingredients.json", jsonIngredients);
                ctx.SpinnerStyle(Style.Parse("red"));
                Thread.Sleep(1000);
        
                // Update the status and spinner
                AnsiConsole.MarkupLine("Saving sandwiches...");
                var jsonSandwiches = JsonConvert.SerializeObject(Sandwiches, Formatting.Indented);
                File.WriteAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Sandwiches.json", jsonSandwiches);
                ctx.SpinnerStyle(Style.Parse("green"));
                Thread.Sleep(1000);
                
                // Update the status and spinner
                AnsiConsole.MarkupLine("Saving sold sandwiches...");
                var jsonSoldSandwiches = JsonConvert.SerializeObject(Sandwiches, Formatting.Indented);
                File.WriteAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\SoldSandwiches.json", jsonSoldSandwiches);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                Thread.Sleep(1000);
                
                // Simulate some work
                AnsiConsole.MarkupLine("Saving finished!");
                Thread.Sleep(2000);
            });
    }

    public void LoadData()
    {
        if (File.Exists(
                "C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Ingredients.json"))
        {
            var jsonIngredients = File.ReadAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Ingredients.json");

            Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(jsonIngredients);
        }
        
        if (File.Exists(
                "C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\SoldSandwiches.json"))
        {
            var jsonSoldSandwiches = File.ReadAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\SoldSandwiches.json");

            SoldSandwiches = JsonConvert.DeserializeObject<List<Sandwich>>(jsonSoldSandwiches);
        }

        if (File.Exists(
                "C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Sandwiches.json"))
        {
            var jsonSandwiches = File.ReadAllText("C:\\Users\\yusuf\\Desktop\\programming\\c#\\bakery-app-console\\BakeryApp.Core\\Sandwiches.json");

            Sandwiches = JsonConvert.DeserializeObject<List<Sandwich>>(jsonSandwiches);
        }
    }

    public void ShowRevenue()
    {
        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Do you want to include VAT?:")
                .PageSize(10)
                .AddChoices("Yes", "No")
                .HighlightStyle(new Style(foreground: Color.Yellow))
        );

        if (selectedOption == "Yes")
        {
            Console.WriteLine(totalRevenue);
        }
        else
        {
            Console.WriteLine(totalRevenue * 0.91);
        }

        foreach (Sandwich soldSandwich in SoldSandwiches)
        {
            Console.WriteLine(soldSandwich.ToString());
        }
        
        Console.ReadLine();
    }
}