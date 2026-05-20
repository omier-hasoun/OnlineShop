

using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListCountryStates;

public sealed record ListCountryStatesQuery : IRequest<Result<List<CountryStatesDto>>>
{
}
