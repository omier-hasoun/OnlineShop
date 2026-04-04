using Domain.Products;

namespace Infrastructure.Data.Configs.Business;

public sealed class ProductConfig : BaseEntityConfig<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new ProductId(value)
               );

        builder.Property(x => x.Title)
               .HasColumnType("VARCHAR(64)")
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("VARCHAR(256)")
               .IsRequired();

        builder.Property(x => x.Brand)
               .HasColumnType("VARCHAR(32)")
               .IsRequired();

        //builder.Property(x => x.Condition)
        //       .HasConversion<byte>()
        //       .IsRequired();

        builder.HasIndex(x => x.Title)
               .HasDatabaseName("IX_Product_Name");

        builder.HasIndex(x => x.Brand)
               .HasDatabaseName("IX_Product_MadeByCompany");

        builder.HasIndex(x => x.Description)
                .HasDatabaseName("IX_Product_Description");

        //builder.HasMany(x => x.ProductImages)
        //       .WithOne()
        //       .HasForeignKey(x => x.ProductId);

        builder.ToTable("Products", x =>
        {
            x.HasCheckConstraint("CK_Product_Rating", "AverageRating between 1 and 5");

        });
    }
}
