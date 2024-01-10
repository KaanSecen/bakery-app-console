namespace BakeryApp.Core;

public class Ingredient
{
    public string Name { get; private set; }
    public double Price { get; private set; }

    public Ingredient(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return this.Name + " - " + this.Price;
    }
}