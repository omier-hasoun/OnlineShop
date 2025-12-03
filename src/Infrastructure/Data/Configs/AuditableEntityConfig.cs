
namespace Infrastructure.Data.Configs;

// inherit for a domain entity that inherits AuditableEntity to apply all the inherited fields configuration
public class AuditableEntityConfig<TEntity> : BaseEntityConfig<TEntity>
where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.CreatedBy)
               .HasColumnType("CHAR(36)")
               .IsRequired();

        builder.Property(x => x.LastModifiedBy)
               .HasColumnType("CHAR(36)")
               .IsRequired();

        builder.Property(x => x.LastModifiedAt)
                .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

    }
}
