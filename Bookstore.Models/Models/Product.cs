using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [DisplayName("List price")]
        [Range(1D,1000D)]
        public double ListPrice { get; set; }
        [Range(1D,1000D)]
        public double Price { get; set; }
        [DisplayName("Price for 50+")]
        public double Price50 { get; set; }
        [DisplayName("Price for 100+")]
        public double Price100 { get; set; }


    }
}
