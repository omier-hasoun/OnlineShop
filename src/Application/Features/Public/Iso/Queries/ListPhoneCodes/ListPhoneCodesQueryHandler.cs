

using Application.Features.Public.Iso.Dtos;

namespace Application.Features.Public.Iso.Queries.ListPhoneCodes;

internal sealed class ListPhoneCodesQueryHandler(IAppDbContext context) : IRequestHandler<ListPhoneCodesQuery, Result<List<PhoneCodeDto>>>
{
    private static List<PhoneCodeDto>? _phoneCodesCache = null;
    public async Task<Result<List<PhoneCodeDto>>> Handle(ListPhoneCodesQuery request, CancellationToken ct)
    {
        _phoneCodesCache ??= await context.Countries.AsNoTracking()
                                                .Select(x => new PhoneCodeDto(x.Code, x.PhoneCode))
                                                .ToListAsync(ct);

        return _phoneCodesCache;
    }
}
