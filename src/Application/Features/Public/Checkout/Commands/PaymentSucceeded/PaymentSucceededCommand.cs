using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Public.Checkout.Commands.PaymentSucceeded;

public sealed record class PaymentSucceededCommand
(
string UserId
) : IRequest;
