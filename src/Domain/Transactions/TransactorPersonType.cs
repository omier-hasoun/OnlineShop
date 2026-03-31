using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Transactions;

public enum TransactorPersonType
{
    Merchant = 1,
    Customer = 2,
    Carrier = 3,
}
