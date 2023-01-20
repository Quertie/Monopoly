namespace Squares
{
    public abstract class Square
    {
        public string Name {get;}

        protected Square(string name)
        {
            Name = name;
        }
        
        // ReSharper disable once PublicConstructorInAbstractClass
        public Square()
        {}
    }
}
