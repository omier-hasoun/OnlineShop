using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class IndexModel : PageModel
{
    public List<Product>
        Products
    { get; set; } = new();
    public int CartItemCount { get; set; }

    public void OnGet()
    {
        Products = GetFeaturedProducts();
        CartItemCount = GetCartItemCount();
    }

    public List<Product> GetFeaturedProducts()
    {
        return new List<Product>
        {
            new Product { Id = 1, Name = "Premium Smartphone", Description = "Latest flagship model with cutting-edge technology and stunning display.", Price = 899, ImageUrl = "📱" },
            new Product { Id = 2, Name = "Ultra Laptop Pro", Description = "High-performance laptop for professionals and creators.", Price = 1499, ImageUrl = "💻" },
            new Product { Id = 3, Name = "Wireless Headphones", Description = "Premium sound quality with active noise cancellation.", Price = 299, ImageUrl = "🎧" },
            new Product { Id = 4, Name = "Smart Watch Elite", Description = "Track your fitness and stay connected with style.", Price = 399, ImageUrl = "⌚" },
            new Product { Id = 5, Name = "Professional Camera", Description = "Capture stunning photos with advanced imaging technology.", Price = 2199, ImageUrl = "📷" },
            new Product { Id = 6, Name = "Gaming Console X", Description = "Next-gen gaming experience with 4K graphics.", Price = 499, ImageUrl = "🎮" }
        };
    }

    private int GetCartItemCount()
    {
        return 3;
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
