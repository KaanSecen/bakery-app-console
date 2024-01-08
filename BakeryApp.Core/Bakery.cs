using Spectre.Console;
namespace BakeryApp.Core;

public class Bakery
{
    public string Name { get; private set; }

    public Bakery(string name)
    {
        Name = name;
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
}