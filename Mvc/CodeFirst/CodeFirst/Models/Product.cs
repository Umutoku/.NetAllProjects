using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Models
{
    [Table("Ürünler")]
    public class Product:BaseEntity
    {
        [Column("Ürün İsmi",TypeName ="nvarchar(50)")]
        [Required,MinLength(5)]
        public string ProductName { get; set; }
        [Column("Birim Fıyatı",TypeName ="numeric(18,1)")]
        public decimal? UnitPrice { get; set; }
        public short UnitInStock { get; set; }
        public int CategoryID { get; set; }
        //Retational Property

        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
