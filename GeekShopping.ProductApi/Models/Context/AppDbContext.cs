﻿using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Models.Context;

public class AppDbContext: DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
	{
	}

	public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 2,
            Name = "Camiseta No Internet",
            Price = new decimal(69.9),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/2_no_internet.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 3,
            Name = "Capacete Darth Vader Star Wars Black Series",
            Price = new decimal(999.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/3_vader.jpg?raw=true",
            CategoryName = "Action Figure"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 4,
            Name = "Star Wars The Black Series Hasbro - Stormtrooper Imperial",
            Price = new decimal(189.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/4_storm_tropper.jpg?raw=true",
            CategoryName = "Action Figure"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 5,
            Name = "Camiseta Gamer",
            Price = new decimal(69.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/5_100_gamer.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 6,
            Name = "Camiseta SpaceX",
            Price = new decimal(49.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/6_spacex.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 7,
            Name = "Camiseta Feminina Coffee Benefits",
            Price = new decimal(69.9),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/7_coffee.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 8,
            Name = "Moletom Com Capuz Cobra Kai",
            Price = new decimal(159.9),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/8_moletom_cobra_kay.jpg?raw=true",
            CategoryName = "Sweatshirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 9,
            Name = "Livro Star Talk – Neil DeGrasse Tyson",
            Price = new decimal(49.9),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/9_neil.jpg?raw=true",
            CategoryName = "Book"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 10,
            Name = "Star Wars Mission Fleet Han Solo Nave Milennium Falcon",
            Price = new decimal(359.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/10_milennium_falcon.jpg?raw=true",
            CategoryName = "Action Figure"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 11,
            Name = "Camiseta Elon Musk Spacex Marte Occupy Mars",
            Price = new decimal(59.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/11_mars.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 12,
            Name = "Camiseta GNU Linux Programador Masculina",
            Price = new decimal(59.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/12_gnu_linux.jpg?raw=true",
            CategoryName = "T-shirt"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 13,
            Name = "Camiseta Goku Fases",
            Price = new decimal(59.99),
            Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.",
            ImageUrl = "https://github.com/nelsonrzjunior/geekshopping/images/13_dragon_ball.jpg",
            CategoryName = "T-shirt"
        });
    }
}