
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configs.Models;

internal sealed class OutboxMessageConfig : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Property(x => x.Type)
               .HasColumnType("VARCHAR(1000)")
               .IsRequired();

        builder.Property(x => x.Content)
               .HasColumnType("NVARCHAR(Max)")
               .IsRequired();

        builder.Property(x => x.OccurredOnUtc)
               .IsRequired();

        builder.Property(x => x.ProcessedOnUtc)
               .IsRequired(false);

        builder.Property(x => x.Error)
               .HasColumnType("NVARCHAR(MAX)")
               .IsRequired(false);

        builder.ToTable("OutboxMessages");
    }
}
