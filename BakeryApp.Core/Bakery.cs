using Spectre.Console;
namespace BakeryApp.Core;

public class Bakery
{
    public string Name { get; private set; }
    public List<BreadType> BreadTypesList { get; private set; }
    public List<Ingredient> Ingredients { get; private set; }

    public Bakery(string name, List<BreadType> breadTypesList)
    {
        Name = name;
        BreadTypesList = breadTypesList;
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
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter the new sandwich name:")
                .PromptStyle(new Style(foreground: Color.Red))
        );

        var breadTypes = BreadTypesList.ToArray();
        
        AnsiConsole.MarkupLine($"Sandwich name: [yellow]{newName}[/]");

        var selectedBreadType = AnsiConsole.Prompt(
            new SelectionPrompt<BreadType>()
                .Title("Choose a bread type:")
                .PageSize(10)
                .AddChoices(BreadTypesList)
                .HighlightStyle(new Style(foreground: Color.Yellow))
        );
        Console.WriteLine(selectedBreadType.ToString());
        Console.ReadLine();
    }
    
    public Ingredient CreateIngredient()
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
        
        Console.WriteLine(newIngredient);
        Console.ReadLine();
        
        return newIngredient;
    }

    public void ShowIngredients()
    {
        foreach (Ingredient ingredient in Ingredients)
        {
            Console.WriteLine(ingredient.ToString());
        }

        Console.ReadLine();
    }
}