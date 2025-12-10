namespace Infrastructure.Data.Configs.Business;

public sealed class ProductConfig : AuditableEntityConfig<Product>
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

        builder.Property(x => x.Price)
               .HasColumnType("DECIMAL(9,2)")
               .IsRequired();

        builder.Property(x => x.Name)
               .HasColumnType("VARCHAR(64)")
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnType("VARCHAR(256)")
               .IsRequired();

        builder.Property(x => x.MadeByCompany)
               .HasColumnType("VARCHAR(32)")
               .IsRequired();

        builder.HasIndex(x => x.Name)
               .HasDatabaseName("IX_Product_Name");

        builder.HasIndex(x => x.MadeByCompany)
               .HasDatabaseName("IX_Product_MadeByCompany");

        builder.HasIndex(x => x.Description)
                .HasDatabaseName("IX_Product_Description");

        builder.HasMany(x => x.ProductImages)
               .WithOne()
               .HasForeignKey(x => x.ProductId);

        builder.ToTable("Products", x =>
        {
            x.HasCheckConstraint("CK_Product_Rating", "AverageRating between 1 and 5");

        });
    }
}
