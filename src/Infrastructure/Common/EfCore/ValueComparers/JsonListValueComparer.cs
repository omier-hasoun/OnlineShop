

namespace Infrastructure.Common.EfCore.ValueComparers;

internal sealed class JsonListValueComparer : ValueComparer<List<string>>
{
    public JsonListValueComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        c => c.ToList())
    {
    }
}
