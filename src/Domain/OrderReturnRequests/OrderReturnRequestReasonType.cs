using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.OrderReturnRequests;

public enum OrderReturnRequestReasonType
{
    None,
    WrongItemSent,
    ItemDamaged,
    ItemNotAsDescribed,
    ChangedMind,
    Other
}
