
using Domain.Products.ProductVariants;
using Domain.Carts.CartItems;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CartItemConfig : BaseEntityConfig<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new CartItemId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.Quantity)
               .IsRequired();

        builder.Property(x => x.ProductVariantId)
               .IsRequired();

        builder.HasOne<ProductVariant>()
               .WithMany()
               .HasForeignKey(x => x.ProductVariantId)
               .IsRequired();

        builder.ToTable("CartItems", x =>
        {
        });

    }
}
