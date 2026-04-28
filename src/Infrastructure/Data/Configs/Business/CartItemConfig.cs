
using Domain.Common.Rules;
using Domain.Customers;
using Domain.Customers.CartItems;
using Domain.Products.ProductVariants;


namespace Infrastructure.Data.Configs.Business;

internal sealed class CartItemConfig : BaseEntityConfig<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new CartItemId(value))
               .ValueGeneratedNever()
               .IsRequired();

        builder.Property(x => x.CustomerId)
               .IsRequired();

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.Property(x => x.ProductVariantId)
               .IsRequired();

        builder.HasOne<Customer>()
               .WithMany(x => x.CartItems)
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();


        builder.HasOne<ProductVariant>()
               .WithMany()
               .HasForeignKey(x => x.ProductVariantId)
               .IsRequired();

        builder.HasIndex(x => new { x.ProductVariantId, x.CustomerId })
               .HasDatabaseName("IX_ProductVariantId_CustomerId")
               .IsUnique();

        builder.ToTable("CartItems", x =>
        {
        });

    }
}
