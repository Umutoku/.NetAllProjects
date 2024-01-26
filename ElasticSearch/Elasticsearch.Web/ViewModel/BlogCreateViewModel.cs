using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Elasticsearch.Web.ViewModel
{
    public class BlogCreateViewModel
    {
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; } = null!;
        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; } = null!;
        [Display(Name = "Tags")]
        public string Tags { get; set; } = null!;
    }
}
