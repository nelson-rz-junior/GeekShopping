using GeekShopping.ProductApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductApi.Models;

[Table("Product")]
public class Product: BaseEntity
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CategoryName { get; set; }

    [Range(1, 10000)]
    public decimal Price { get; set; }

    [StringLength(300)]
    public string? ImageUrl { get; set; }
}
