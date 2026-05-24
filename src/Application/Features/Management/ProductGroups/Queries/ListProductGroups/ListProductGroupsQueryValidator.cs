
namespace Application.Features.Management.ProductGroups.Queries.ListProductGroups;

internal sealed class ListProductGroupsQueryValidator : AbstractValidator<ListProductGroupsQuery>
{
    public ListProductGroupsQueryValidator()
    {
        RuleFor(x => x.SearchText)
            .Must(text => string.IsNullOrEmpty(text) || text.Length <= ProductGroupRules.MaxTitleLength);

        RuleFor(x => x.Size)
            .LessThanOrEqualTo(50);
    }
}
