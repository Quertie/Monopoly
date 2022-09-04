public abstract class Deed:Square
{
    public int Price { get; }
    
    public Deed(string name, int price):base(name)
    {
        Price = price;
    }
}
