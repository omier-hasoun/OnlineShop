
namespace Domain.Categories;

public class Category
{
    private Category()
    {
    
    }
    
    public static Result<Category> Create(CategoryId id, string name, CategoryId? parentCategoryId = null)
    {
        return new Category
        {
            Id = id,
            Name = name,
            ParentCategoryId = parentCategoryId,
        };
    }
    
    public CategoryId Id { get; private init; }
    public CategoryId? ParentCategoryId { get; private set; }
    public string Name { get; private set; } = null!;
}
