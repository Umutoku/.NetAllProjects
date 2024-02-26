namespace WebApp.Composite.Composites
{
    public interface IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        int Count(); // Count the number of the components
        string Display(); // Display the name of the component
    }
}
