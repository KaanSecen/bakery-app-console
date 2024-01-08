namespace BakeryApp.Core;

public class Sandwich
{
    public string Name { get; private set; }
    public double BasePrice { get; private set; }
    public BreadType BreadType { get; private set; }

    public Sandwich(string name, double basePrice, BreadType breadType)
    {
        Name = name;
        BasePrice = basePrice;
        BreadType = breadType;
    }
}