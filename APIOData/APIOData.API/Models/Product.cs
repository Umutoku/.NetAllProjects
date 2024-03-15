using OData.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyAPIOData.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //[ForeignKey("Category")]
        public int CategoryId { get; set; }
        public DateTime? Created { get; set; }
        public virtual Category Category { get; set; }  // virtual keyword is used to enable lazy loading // virtual keyword kullanımı ile tembel yükleme etkinleştirilir

        public int? FeatureId { get; set; } // ? işareti ile nullable bir alan olduğu belirtilir. Tablo sonradan oluşturulduğunda hata almamak için kullanılır.
        public virtual Feature Feature { get; set; }
    }
}
