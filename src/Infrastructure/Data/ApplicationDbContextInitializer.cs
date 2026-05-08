
using Domain.Brands;
using Domain.Categories;
using Domain.Common.ValueObjects;
using Domain.Products;
using Domain.Products.ProductVariants;
using Domain.Products.ValueObjects;
using Microsoft.Extensions.Logging;
using static Domain.DomainErrors;

namespace Infrastructure.Data;

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, AppDbContext context, UserManager<AppUser> userManager, RoleManager<Role> roleManager)
{
    public async Task InitialiseAndSeedData()
    {
        try
        {
            //await context.Database.EnsureDeletedAsync();
            if(!await context.Database.EnsureCreatedAsync())
                await SeedData();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
        }
    }

    private async Task SeedData()
    {
        var user1 = new AppUser()
        {
            Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
            Email = "om@gmail.com",
            NormalizedEmail = "om@gmail.com".ToUpper(),
            UserName = "om@gmail.com",
            NormalizedUserName = "om@gmail.com".ToUpper(),
            EmailConfirmed = true,
            AccessFailedCount = 0
        };
        await userManager.CreateAsync(user1, "1234");


        var brandId1 = new BrandId(Guid.Parse("019dd6b8-96d0-734b-97ca-02b8094e7f00"));
        var brandId2 = new BrandId(Guid.Parse("019dd6b8-96d0-7904-8a7a-d1e750789a04"));

        context.Brands.Add(Brand.Create(brandId1, "Apple", "shity company", "Apple GmbH", "APP", null).Value);
        context.Brands.Add(Brand.Create(brandId2, "Nvidia", "shity company", "NVidia GmbH", "NVIDIA", null).Value);

        var categoryId1 = new CategoryId(1);
        var categoryId2 = new CategoryId(2);

        context.Categories.Add(Category.Create(categoryId1, "Smart phones", null).Value);
        context.Categories.Add(Category.Create(categoryId2, "Books", null).Value);

        var productId1 = new ProductId(3);
        var productId2 = new ProductId(4);
        var product1 = Product.Create(productId1, brandId1, categoryId1, "Iphone 17 pro max", "A shitty phone", true, new Dictionary<string, string>() { { "Ram", "8Gb" }, { "Display", "17 Zoll" } }.AsReadOnly()).Value;
        var product2 = Product.Create(productId2, brandId2, categoryId2, "Iphone 17 pro", "A shitty phone", true, new Dictionary<string, string>() { { "Ram", "6Gb" }, { "Display", "14 Zoll" } }.AsReadOnly()).Value;


        var productVariantId1 = new ProductVariantId(5);
        var productVariantId2 = new ProductVariantId(6);
        var productVariantId3 = new ProductVariantId(7);
        var productVariantId4 = new ProductVariantId(8);

        product1.AddVariant(productVariantId1, Money.From(200).Value, 20, 20, 20, 2, "OMIERHASOUNKAMHEA", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "blue" } });
        product1.AddVariant(productVariantId2, Money.From(220).Value, 10, 10, 10, 2, "OMIERHASOUNKAMHEA", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "pink" } });
        product2.AddVariant(productVariantId3, Money.From(160).Value, 10, 10, 10, 2, "ommei30273", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "pink" } });
        product2.AddVariant(productVariantId4, Money.From(150).Value, 10, 10, 10, 2, "ommei30272", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "gold" } });


        context.Products.Add(product1);
        context.Products.Add(product2);

        
        await context.SaveChangesAsync();
    }


}
