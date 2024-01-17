namespace BakeryApp.Core;

public class Sandwich
{
    public string Name { get; private set; }
    public double BasePrice { get; private set; }
    public BreadType BreadType { get; private set; }
    
    public List<Ingredient> Ingredients { get; private set; }

    public Sandwich(string name, double basePrice, BreadType breadType, List<Ingredient> ingredients)
    {
        Name = name;
        BasePrice = basePrice;
        BreadType = breadType;
        Ingredients = ingredients;
    }

    public override string ToString()
    {
        var ingredientsString = string.Join(", ", Ingredients.Select(i => i.Name));
        return $"{Name} - {BasePrice} - {BreadType} - {ingredientsString}";
    }

    public double GetPrice()
    {
        return BasePrice + Ingredients.Sum(ingredient => ingredient.Price);
    }
}