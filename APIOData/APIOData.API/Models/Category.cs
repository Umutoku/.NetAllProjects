namespace UdemyAPIOData.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } //ICollection kullanımı ile listten farklı olarak 
        //ekleme, silme, güncelleme gibi işlemler yapılabilir.
    }
}
