

using Domain.Products;
using Domain.Products.ProductImages;
using Domain.Products.ProductVariants;

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

        builder.Property(x => x.FileName)
               .HasColumnType("VARCHAR(40)")// the file name should be a guid + . + extension
               .IsRequired();

        builder.Property(x => x.FileSize)// in bytes
               .HasColumnType("INT")
               .IsRequired();

        builder.HasOne<ProductVariant>()
               .WithMany()
               .HasForeignKey(x => x.ProductVariantId)
               .IsRequired(false);

        builder.HasOne<Product>()
               .WithMany(x => x.Images)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();


        builder.ToTable("ProductImages", x =>
        { 

        });
    }


}
