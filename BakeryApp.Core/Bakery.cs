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

    public void CreateSandwich()
    {
        var lessThanFiveIngredients = true;
        List<Ingredient> ingredients = null;
        
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the new sandwich name:")
                .PromptStyle(new Style(foreground: Color.Red))
        );
        
        AnsiConsole.MarkupLine($"Sandwich name: [yellow]{newName}[/]");
        
        var newPrice = AnsiConsole.Prompt(
            new TextPrompt<double>("Enter the new sandwich base price:")
                .PromptStyle(new Style(foreground: Color.Red))
        );
        
        AnsiConsole.MarkupLine($"Sandwich base price: [yellow]{newPrice}[/]");

        var selectedBreadType = AnsiConsole.Prompt(
            new SelectionPrompt<BreadType>()
                .Title("Choose a bread type:")
                .PageSize(10)
                .AddChoices(BreadTypesList)
                .HighlightStyle(new Style(foreground: Color.Yellow))
        );
        
        AnsiConsole.MarkupLine($"Sandwich bread: [yellow]{selectedBreadType}[/]");

        while (lessThanFiveIngredients)
        {
            ingredients = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Ingredient>()
                    .Title("Choose your [green]Ingredients[/]?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more ingredients)[/]")
                    .InstructionsText("Choose up to 5 ingredients")
                    .AddChoices(Ingredients.Select(i => i)).UseConverter(ingredient => ingredient.ToString()));

            if (ingredients.Count <= 5)
            {
                lessThanFiveIngredients = false;
            }
        }

        var newSandwich = new Sandwich(newName, newPrice, selectedBreadType, ingredients);
        Sandwiches.Add(newSandwich);
        
        Console.WriteLine(newSandwich.ToString());
        Console.ReadLine();
    }
    
    public void CreateIngredient()
    {
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the new ingredient name:")
                .PromptStyle(new Style(foreground: Color.Red))
        );
        
        AnsiConsole.MarkupLine($"Ingredient name: [yellow]{newName}[/]");
        
        var newPrice = AnsiConsole.Prompt(
            new TextPrompt<double>("Enter the new ingredient price:")
                .PromptStyle(new Style(foreground: Color.Red))
        );

        var newIngredient = new Ingredient(newName, newPrice);
        Ingredients.Add(newIngredient);
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

    public void SellSandwich()
    {
        var selectedSandwiches = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Sandwich>()
                .Title("Choose your [green]Sandwiches[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more sandwiches)[/]")
                .AddChoices(Sandwiches.Select(i => i)));
        
        Console.WriteLine("-----------------------------");
        foreach (Sandwich sandwich in selectedSandwiches)
        {
            Console.WriteLine(sandwich.BasePrice);
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine("You have selected:");
        foreach (Sandwich sandwich in selectedSandwiches)
        {
            Console.WriteLine(sandwich.ToString());
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine("The prices are:");
        foreach (Sandwich sandwich in selectedSandwiches)
        {
            Console.WriteLine(sandwich.GetPrice());
        }
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Your total is:");
        Console.WriteLine(CalculatePrice(selectedSandwiches));

        totalRevenue += CalculatePrice(selectedSandwiches);

        foreach (Sandwich sandwich in selectedSandwiches)
        {
            SoldSandwiches.Add(sandwich);
        }
        
        Console.ReadLine();
    }
}