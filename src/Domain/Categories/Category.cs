
namespace Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    private Category(CategoryId id, string name, CategoryId? parentCategoryId) : base(id)
    {
        Name = name;
        ParentCategoryId = parentCategoryId;
    }
    
    public static Result<Category> Create(CategoryId id, string name, CategoryId? parentCategoryId)
    {
        return new Category(id, name, parentCategoryId);
    }
    
    public CategoryId? ParentCategoryId { get; private set; }
    public string Name { get; private set; } = null!;
}
