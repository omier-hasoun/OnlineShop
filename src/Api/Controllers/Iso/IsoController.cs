
using Application.Features.Public.Iso.Queries.ListCountries;
using Application.Features.Public.Iso.Queries.ListCountryStates;
using Application.Features.Public.Iso.Queries.ListPhoneCodes;

namespace Api.Controllers.Iso;

[Route("iso")]
public sealed class IsoController(IMediator mediator) : ApiController
{
    [HttpGet("countries")]
    public async Task<IActionResult> ListCountries(CancellationToken ct)
    {
        ListCountriesQuery request = new();
        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet("phone-codes")]
    public async Task<IActionResult> ListPhoneCodes(CancellationToken ct)
    {
        ListPhoneCodesQuery request = new();
        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }

    [HttpGet("countries/states")]
    public async Task<IActionResult> ListCountryStates(CancellationToken ct)
    {
        ListCountryStatesQuery request = new();
        var result = await mediator.Send(request, ct);

        return result.Match((response) => Ok(response), Problem);
    }
}
