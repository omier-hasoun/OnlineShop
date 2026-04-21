
using Domain.Customers.CartItems;


namespace Infrastructure.Data.Configs.Business;

public sealed class CartItemConfig : BaseEntityConfig<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => new CartItemId(value))
               .ValueGeneratedNever();

        builder.ToTable("CartItems", x =>
        {
            x.HasCheckConstraint("CK_CartItems_Quantity", "[Quantity] between 1 and 2000");
        });

    }
}
