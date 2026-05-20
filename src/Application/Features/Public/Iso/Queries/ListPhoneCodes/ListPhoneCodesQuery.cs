
using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListPhoneCodes;

public sealed record ListPhoneCodesQuery : IRequest<Result<List<PhoneCodeDto>>>
{
}
