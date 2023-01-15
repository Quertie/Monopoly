namespace Squares
{
    public class Property: Deed
    {
        public ColorGroup ColorGroup;
        public Property(string name, ColorGroup colorGroup, int price):base(name, price)
        {
            ColorGroup = colorGroup;
            ColorGroup.AddProperty(this);
        }
    }
}
