
using Application.Entities;
using Domain.Carts;
using Domain.Common.ValueObjects;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CartConfig : BaseEntityConfig<Cart>
{
    public override void Configure(EntityTypeBuilder<Cart> builder)
    {
        base.Configure(builder);

        builder.HasKey(e => e.Id)
               .IsClustered();

        builder.Property(e => e.Id)
               .HasConversion(x => x.Value, value => new CartId(value))
               .ValueGeneratedNever();

        builder.Property(x => x.GuestId)
               .HasConversion(x => x!.Value.Value, value => new GuestAccountId(value))
               .ValueGeneratedNever();

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(x => x.CartId)
               .IsRequired();

        builder.HasOne<AppUser>()
               .WithOne()
               .HasForeignKey<Cart>(x => x.UserId)
               .IsRequired(false); 

        builder.ToTable("Carts", x =>
        {
        });

    }
}
