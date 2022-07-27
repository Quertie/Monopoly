public class Tax:Square
{
    public int amountDue {get;}
    public Tax(string name, int amountDue):base(name)
    {
        this.amountDue = amountDue;
    }
}