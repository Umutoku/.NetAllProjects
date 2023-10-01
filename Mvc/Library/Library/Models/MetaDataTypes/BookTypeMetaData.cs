using System.ComponentModel.DataAnnotations;

namespace Library.Models.MetaDataTypes
{
    public class BookTypeMetaData
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        [MaxLength(15, ErrorMessage = "Maksimum 15 karakter")]
        public string Name { get; set; }
    }
}
