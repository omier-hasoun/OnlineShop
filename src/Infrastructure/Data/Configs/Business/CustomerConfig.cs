
namespace Infrastructure.Data.Configs.Business;

public sealed class CustomerConfig : BaseEntityConfig<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasConversion(
                   id => id.Value,
                   value => new CustomerId(value)
               );

        builder.Property(x => x.FirstName)
               .HasColumnType("VARCHAR(32)")
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasColumnType("VARCHAR(32)")
               .IsRequired();

        builder.Property(x => x.Email)
               .HasColumnType("VARCHAR(254)")
               .IsRequired();

        builder.Property(x => x.PhoneNumber)
               .HasColumnType("VARCHAR(16)")
               .IsRequired(false);

        builder.HasIndex(x => new { x.FirstName, x.LastName })
               .HasDatabaseName("IX_Customer_FullName");

        builder.HasIndex(x => x.Email)
               .HasDatabaseName("IX_Customer_Email");

        builder.ToTable("Customers");
    }
}
