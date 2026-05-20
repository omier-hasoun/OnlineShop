
using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListCountries;

public sealed record ListCountriesQuery : IRequest<Result<List<CountryDto>>>
{
}
