namespace Infrastructure.Data.Configs.Business;

public sealed class ReviewConfig : BaseEntityConfig<Review>
{
    public override void Configure(EntityTypeBuilder<Review> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd()
               .HasConversion(
                   id => id.Value,
                   value => new ReviewId(value)
               );

        builder.Property(x => x.Comment)
               .HasColumnType("NVARCHAR(128)")
               .IsRequired();

        builder.HasOne(x => x.ProductInfo)
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.ProductId)
               .IsRequired();

        builder.HasOne(x => x.CustomerInfo)
               .WithMany()
               .HasForeignKey(x => x.CustomerId)
               .IsRequired();

        builder.ToTable("Reviews", x =>
        {
            x.HasCheckConstraint("CK_Review_Rating", "Rating between 1 and 5");

        });
    }
}
