using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyEFCore.CodeFirst.DAL
{
    //[Table("ProductTb",Schema ="products")] //data annontications

    public class Product
    {
        //[Key]
        public int Id { get; set; }
        //[Column("name2",Order =1,TypeName = "nvarchar(50)")] // tipi belirleniyor isim veriliyor. Data ann //order hangi sırada olacağını belirler
        //[MaxLength(100)] // max karakter, genel string için
        //[StringLength(50,MinimumLength =10)] // minimum karakter sayısı
        public string Name { get; set; }
        //[Column(Order =2,TypeName ="decimal(18,2)")] //topla oluştuktan sonra sıralama önemli değil 
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Barcode { get; set; }
        //public DateTime? CreatedDate { get; set; }
        public int CategoryId { get; set; }
        //Navgation property
        //[ForeignKey(nameof(CategoryId))]// migration öncesi için
        public Category Category { get; set; }
        public ProductFeature ProductFeature { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]         //Sadece ilk veri girdiğinde erişilebilsin
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]         //Sql tarafında oluşturulacak
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]             //Hiç bir şekilde oluşturulmasın
        //[]
        public DateTime CreatedDate { get; set; }  = DateTime.Now;
    }
}
