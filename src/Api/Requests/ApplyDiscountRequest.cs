namespace Api.Requests;

public sealed record ApplyDiscountRequest(DateOnly DiscountExpiresOn, byte DiscountPercentage);

