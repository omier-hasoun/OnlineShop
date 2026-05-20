
namespace Domain.Countries.StateProvinces;

public sealed class StateProvince : IEntity
{
    public StateProvince()
    {
        
    }
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public int CountryId { get; init; }



}
