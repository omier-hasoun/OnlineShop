
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages;

public class Index : PageModel
{
    public List<Product> Products { get; set; }

    public void OnGet()
    {
        // Sample products
        Products = new List<Product>
        {
            new Product { Name = "Product 1", Price = 19.99M, ImageUrl = "https://via.placeholder.com/200" },
            new Product { Name = "Product 2", Price = 29.99M, ImageUrl = "https://via.placeholder.com/200" },
            new Product { Name = "Product 3", Price = 39.99M, ImageUrl = "https://via.placeholder.com/200" }
        };
    }
    public IActionResult OnGetRegister()
    {
        return Redirect("/Account/Register"); 
    }
}

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
}
