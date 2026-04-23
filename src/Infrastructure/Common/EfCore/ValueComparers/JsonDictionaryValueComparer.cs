

namespace Infrastructure.Common.EfCore.ValueComparers;

internal class JsonDictionaryValueComparer : ValueComparer<Dictionary<string, string>>
{
    public JsonDictionaryValueComparer() : base(
        (d1, d2) => d1.Count == d2.Count && !d1.Except(d2).Any(),
        d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
        d => d.ToDictionary(entry => entry.Key, entry => entry.Value))
    {
    }
}
