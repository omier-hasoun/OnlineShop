
namespace Infrastructure.Data.Configs.Base;

// inherit for a domain entity that inherit BaseEntity to apply all the inherited fields configuration
internal abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Ignore(x => x.DomainEvents);
    }
}
