
using Domain.Categories;
using Domain.Common.Rules;

namespace Infrastructure.Data.Configs.Business;

internal sealed class CategoryConfig : BaseEntityConfig<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, value => CategoryId.From(value).Value)
               .ValueGeneratedNever();

        builder.Property(x => x.Name)
               .HasMaxLength(CategoryRules.MaxCategoryNameLength)
               .IsRequired();

        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(x => x.ParentCategoryId);

        builder.ToTable("Categories");

    }
}
