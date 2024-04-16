namespace EventSourcing.API.Models
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; set; }
        public int UserId { get; set; }
    }
}
