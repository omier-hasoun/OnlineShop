
namespace Infrastructure.Data.Configs;

// inherit for a domain entity that inherit BaseEntity to apply all the inherited fields configuration
public class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever();

        builder.Ignore(x => x.DomainEvents);
    }
}
