
namespace Infrastructure.Data.Configs.Abstractions;

// inherit for a domain entity that inherit BaseEntity to apply all the inherited fields configuration
public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Ignore(x => x.DomainEvents);
        builder.UseTpcMappingStrategy();
    }
}
