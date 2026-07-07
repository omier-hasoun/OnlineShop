using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Public.Checkout.Dtos;

public enum  RefundState
{
    Succeeded,
    Failed,
    ActionRequired,
    Canceled
}
