
using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListCountryStates;

internal sealed class ListCountryStatesQueryHandler(IAppDbContext context) : IRequestHandler<ListCountryStatesQuery, Result<List<CountryStatesDto>>>
{
    private static List<CountryStatesDto>? _stateProvincesCache = null;

    public async Task<Result<List<CountryStatesDto>>> Handle(ListCountryStatesQuery request, CancellationToken ct)
    {
        var data = await context.StateProvinces
                                .AsNoTracking()
                                .Join(
                                    context.Countries,
                                    s => s.CountryId,
                                    c => c.Id,
                                    (state, country) => new
                                    {
                                        StateName = state.Name,
                                        CountryId = state.CountryId,
                                        CountryCode = country.Code
                                    }
                                )
                                .ToListAsync(ct);

        _stateProvincesCache = data
            .GroupBy(x => new { x.CountryId, x.CountryCode })
            .Select(g => new CountryStatesDto(
                CountryCode: g.Key.CountryCode,
                States: g
                    .OrderBy(x => x.StateName)
                    .Select(x => x.StateName)
                    .ToList()
            ))
            .OrderBy(x => x.CountryCode)
            .ToList();


        return _stateProvincesCache;
    }
}
