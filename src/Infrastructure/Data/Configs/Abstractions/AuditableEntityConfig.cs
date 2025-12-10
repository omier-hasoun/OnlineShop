namespace Infrastructure.Data.Configs.Abstractions;

// inherit for a domain entity that inherits AuditableEntity to apply all the inherited fields configuration
public abstract class AuditableEntityConfig<TEntity> : BaseEntityConfig<TEntity>
where TEntity : AuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

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
