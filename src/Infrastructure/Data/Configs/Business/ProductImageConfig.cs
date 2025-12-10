namespace Infrastructure.Data.Configs.Business;

public sealed class ProductImageConfig : BaseEntityConfig<ProductImage>
{
    public override void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new ProductImageId(value)
               );

        builder.ToTable("ProductImages", x =>
        {
            x.HasCheckConstraint("CK_ProductImage_SortOrder", "SortOrder between 1 and 32");

        });
    }


}
