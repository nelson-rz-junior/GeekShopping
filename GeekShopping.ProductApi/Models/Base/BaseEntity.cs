using System.ComponentModel.DataAnnotations;

namespace GeekShopping.ProductApi.Models.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}
