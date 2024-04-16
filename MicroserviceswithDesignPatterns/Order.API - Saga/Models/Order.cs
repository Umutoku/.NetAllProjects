namespace Order.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public DateTime CreatedDate { get; set; }
        // diğer modelde owned olduğu için burada navigation property olarak kullanılabiliyor
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus Status { get; set; }
        public string FailMessage { get; set; }
    }

    public enum OrderStatus
    {
        Suspend,
        Completed,
        Failed
    }
}
