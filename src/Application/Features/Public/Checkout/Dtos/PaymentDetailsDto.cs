
using Domain.Common.ValueObjects;

namespace Application.Features.Public.Checkout.Dtos;

public sealed record PaymentDetailsDto(
string OrderId,
long TaxAmount,
long TotalAmount,
string PaymentMethodFingerPrint,
string PaymentMethodType,
string PaymentId,
PaymentState PaymentStatus,
AddressDetails BillingAddress,
AddressDetails ShippingAddress,
string Email
);
