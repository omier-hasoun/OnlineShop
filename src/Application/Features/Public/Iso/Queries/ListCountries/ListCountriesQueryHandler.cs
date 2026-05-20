

using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListCountries;

internal sealed class ListCountriesQueryHandler(IAppDbContext context) : IRequestHandler<ListCountriesQuery, Result<List<CountryDto>>>
{
    private static List<CountryDto>? _countriesCache = null;
    public async Task<Result<List<CountryDto>>> Handle(ListCountriesQuery request, CancellationToken ct)
    {
        _countriesCache ??= await context.Countries.AsNoTracking()
                                               .Select(x => new CountryDto(x.Code, x.Name))
                                               .ToListAsync(ct);
        return _countriesCache;
    }
}
