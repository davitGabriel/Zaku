using System;
using System.Collections.Generic;
using System.Text;

namespace Zaku.Domain.Enums
{
    public enum OrderType
    {
        Direct = 1,
        OpenRequest = 2
    }

    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        InProgress = 3,
        Delivered = 4,
        Cancelled = 5
    }
}
