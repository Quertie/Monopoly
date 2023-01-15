using System.Collections.Generic;
using System.Drawing;
using Squares;

public class ColorGroup
{
    private readonly List<Property> _properties = new List<Property>();
    public Color Color {get;}

    public List<Property> Properties => _properties;

    public ColorGroup(Color color)
    {
        Color = color;
    }

    public void AddProperty(Property property)
    {
        Properties.Add(property);
    }

}