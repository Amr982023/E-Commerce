using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Consts
{
    public enum enOrderStatus
    {
        Pending = 1,
        Processing = 2,
        Confirmed = 3,
        Packed = 4,
        Shipped = 5,
        OutForDelivery = 6,
        Delivered = 7,
        Cancelled = 8,
        Returned = 9,
        Refunded = 10,
        FailedPayment = 11,
        OnHold = 12
    }
}
