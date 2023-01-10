﻿namespace GeekShopping.Web.Models;

public class ProductViewModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? CategoryName { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }
}
