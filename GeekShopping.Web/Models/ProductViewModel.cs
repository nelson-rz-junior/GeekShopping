using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.Models;

public class ProductViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The name is required")]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Category")]
    public string? CategoryName { get; set; }

    [Required(ErrorMessage = "The price is required")]
    [Range(1, 10000, ErrorMessage = "The price must be between {1} and {2}")]
    public decimal Price { get; set; }

    [Display(Name = "Image Url")]
    public string? ImageUrl { get; set; }
}
