using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Consts
{
    public enum PaymentType
    {
        CashOnDelivery = 1,
        CreditCard = 2,
        DebitCard = 3,
        PayPal = 4,
        ApplePay = 5,
        GooglePay = 6,
        Wallet = 7,
        BankTransfer = 8
    }
}
