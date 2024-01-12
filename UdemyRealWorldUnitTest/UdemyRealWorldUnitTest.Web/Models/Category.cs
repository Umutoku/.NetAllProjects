namespace UdemyRealWorldUnitTest.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } // virtual keyword is used for lazy loading
    }
}
