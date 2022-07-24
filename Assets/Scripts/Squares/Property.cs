public class Property:Square
{
    public ColorGroup ColorGroup {get;}

    public int Price {get;}

    public Property(string name, ColorGroup colorGroup, int price):base(name)
    {
        ColorGroup = colorGroup;
        ColorGroup.AddProperty(this);
        Price = price;
    }
}
