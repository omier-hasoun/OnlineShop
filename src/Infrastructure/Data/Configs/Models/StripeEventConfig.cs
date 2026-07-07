
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configs.Models;

internal sealed class StripeEventConfig : IEntityTypeConfiguration<StripeEvent>
{
    public void Configure(EntityTypeBuilder<StripeEvent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.StripeSessionId)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.StripeEventId)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>()
               .IsRequired();

        builder.Property(x => x.Type)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.HasIndex(x => x.StripeEventId)
               .IsUnique();

        builder.Property(x => x.StripeEventId)
               .HasColumnType("VARCHAR(255)")
               .IsRequired();

        builder.ToTable("StripeEvents");
    }
}
