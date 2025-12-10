namespace Infrastructure.Data.Configs.Business;

public sealed class CartItemConfig : BaseEntityConfig<CartItem>
{
    public override void Configure(EntityTypeBuilder<CartItem> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => new { x.CustomerId, x.ProductId });


        builder.HasOne<Customer>()
               .WithMany(x => x.CartItems)
               .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.ProductInfo)
               .WithOne()
               .HasForeignKey<CartItem>(x => x.ProductId);

        builder.ToTable("CartItems");
    }
}
