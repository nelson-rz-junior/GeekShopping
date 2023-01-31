using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public class ProductViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    [StringLength(150)]
    public string Name { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Display(Name = "Category")]
    [StringLength(50)]
    public string? CategoryName { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [RegularExpression(@"^(\d{1,10}(,\d{0,2})?)$", ErrorMessage = "The price is invalid")]
    public decimal Price { get; set; }

    [Display(Name = "Image Url")]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
}
