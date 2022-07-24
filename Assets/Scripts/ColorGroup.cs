using System.Collections.Generic;
using System.Drawing;

public class ColorGroup
{
    public Color Color {get;}
    private List<Property> _properties = new List<Property>();
    public List<Property> Properties {
        get
        {
            return _properties;
        }
    }

    public ColorGroup(Color color)
    {
        Color = color;
    }

    public void AddProperty(Property property)
    {
        _properties.Add(property);
    }

}