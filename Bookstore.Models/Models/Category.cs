using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models.Models;

public class Category
{
    // primarni kljuc
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [DisplayName("Display Order")]

    [Range(0,30,ErrorMessage="This is the range error")]
    public int DisplayOrder { get; set; }
}
