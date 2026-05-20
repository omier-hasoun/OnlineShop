
using Application.Entities;
using Domain.Brands;
using Domain.Carts;
using Domain.Carts.CartItems;
using Domain.Categories;
using Domain.Common.ValueObjects;
using Domain.Currencies;
using Domain.ProductsGroups;
using Domain.ProductsGroups.Products;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, AppDbContext context, UserManager<AppUser> userManager, RoleManager<Role> roleManager)
{
    public async Task InitialiseAndSeedData()
    {
        try
        {
            //await context.Database.MigrateAsync();
            
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

        await SeedCurrencies();

        var brandId1 = new BrandId(Guid.Parse("019dd6b8-96d0-734b-97ca-02b8094e7f00"));
        var brandId2 = new BrandId(Guid.Parse("019dd6b8-96d0-7904-8a7a-d1e750789a04"));

        context.Brands.Add(Brand.Create(brandId1, "Apple", "shity company", "Apple GmbH", "APP", null).Value);
        context.Brands.Add(Brand.Create(brandId2, "Nvidia", "shity company", "NVidia GmbH", "NVIDIA", null).Value);

        var categoryId1 = new CategoryId(1);
        var categoryId2 = new CategoryId(2);

        context.Categories.Add(Category.Create(categoryId1, "Smart phones", null).Value);
        context.Categories.Add(Category.Create(categoryId2, "Books", null).Value);

        var productGroupId1 = new ProductGroupId(3);
        var productGroupId2 = new ProductGroupId(4);
        var productGroup1 = ProductsGroup.Create(productGroupId1, brandId1, categoryId1, "Iphone 17 pro max", "A shitty phone", true, new Dictionary<string, string>() { { "Ram", "8Gb" }, { "Display", "17 Zoll" } }).Value;
        var productGroup2 = ProductsGroup.Create(productGroupId2, brandId2, categoryId2, "Iphone 17 pro", "A shitty phone", true, new Dictionary<string, string>() { { "Ram", "6Gb" }, { "Display", "14 Zoll" } }).Value;


        var productId1 = new ProductId(5);
        var productId2 = new ProductId(6);
        var productId3 = new ProductId(7);
        var productId4 = new ProductId(8);

        productGroup1.AddProduct(productId1, Money.From(200).Value, 20, 20, 20, 2, "OMIERHASOUNKAMHEA", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "blue" } });
        productGroup1.AddProduct(productId2, Money.From(220).Value, 10, 10, 10, 2, "OMIERHASOUNKAMHEA", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "pink" } });
        productGroup2.AddProduct(productId3, Money.From(160).Value, 10, 10, 10, 2, "ommei30273", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "pink" } });
        productGroup2.AddProduct(productId4, Money.From(150).Value, 10, 10, 10, 2, "ommei30272", "omier-hasoun.com", "1234567890", new Dictionary<string, string>() { { "Color", "gold" } });


        context.ProductGroups.Add(productGroup1);
        context.ProductGroups.Add(productGroup2);

        var cart1 = Cart.CreateForGuest(new CartId(20), new GuestAccountId(Guid.Parse("019e0f1a-7b38-79e5-8912-9a9f83a4a549"))).Value;
        var cart2 = Cart.CreateForUser(new CartId(21), Guid.Parse("10000000-0000-0000-0000-000000000001")).Value;


        cart1.AddItem(new CartItemId(10), productId2, 3);
        cart1.AddItem(new CartItemId(11), productId4, 1);

        cart2.AddItem(new CartItemId(12), productId3, 2);
        cart2.AddItem(new CartItemId(112), productId4, 10);


        context.Carts.Add(cart1);
        context.Carts.Add(cart2);



        await context.SaveChangesAsync();
    }













    private async Task SeedCurrencies()
    {
        context.Currencies.Add(new Currency
        {
            Code = "USD",
            Name = "Dollars",
            Symbol = "$"

        });
        context.Currencies.Add(new Currency
        {
            Code = "EUR",
            Name = "Euro",
            Symbol = "€"

        });

        context.Currencies.Add(new Currency
        {
            Code = "KR",
            Name = "Swedish krona",
            Symbol = "kr"

        });

        context.Currencies.Add(new Currency
        {
            Code = "GBP",
            Name = "Pounds",
            Symbol = "£"

        });

        context.Currencies.Add(new Currency
        {
            Code = "SYP",
            Name = "Pounds",
            Symbol = "£"

        });
    }
}
