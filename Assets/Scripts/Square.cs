public abstract class Square
{
    public string Name {get;}

    public Square(string name)
    {
        Name = name;
    }
}

public class Property:Square
{
    public ColorGroup ColorGroup {get;}

    public Property(string name, ColorGroup colorGroup):base(name)
    {
        ColorGroup = colorGroup;
        ColorGroup.AddProperty(this);
    }
}
public class CommunityChestSquare:Square
{
    public CommunityChestSquare(string name):base(name)
    {
    }
}