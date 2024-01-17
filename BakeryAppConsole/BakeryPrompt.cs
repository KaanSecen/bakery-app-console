using BakeryApp.Core;
using Spectre.Console;

namespace BakeryAppConsole;

public class BakeryPrompt
{
    public List<BreadType> BreadTypesList { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }
    public List<Sandwich> Sandwiches { get; private set; }
    public Bakery Bakery { get; private set; }

    public BakeryPrompt(List<BreadType> breadTypesList, List<Ingredient> ingredients, List<Sandwich> sandwiches, Bakery bakery)
    {
        BreadTypesList = breadTypesList;
        Ingredients = ingredients;
        Sandwiches = sandwiches;
        Bakery = bakery;
    }

    public string GetNewSandwichName()
    {
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the new sandwich name:")
                .PromptStyle(new Style(foreground: Color.Red))
        );

        return newName;
    }
    
    public Sandwich PromptSandwichCreation()
    {
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
        
        var ingredients = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Ingredient>()
                .Title("Choose your [green]Ingredients[/]?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more ingredients)[/]")
                .AddChoices(Ingredients.Select(i => i)));

        var newSandwich = new Sandwich(newName, newPrice, selectedBreadType, ingredients);
        Sandwiches.Add(newSandwich);
        
        Console.WriteLine(newSandwich.ToString());
        Console.ReadLine();

        return newSandwich;
    }

    public void PromptSellSandwich()
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
        Console.WriteLine(Bakery.CalculatePrice(selectedSandwiches));

        Bakery.totalRevenue += Bakery.CalculatePrice(selectedSandwiches);

        foreach (Sandwich sandwich in selectedSandwiches)
        {
            Bakery.SoldSandwiches.Add(sandwich);
        }
        
        Console.ReadLine();
    }

    public Ingredient PromptIngredientCreation()
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

        return new Ingredient(newName, newPrice);
    }
}