// Filename: CurrencyExtensions.cs
// Date Created: 2020-02-10
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;

namespace eDoxa.Payment.Api.Extensions
{
    public static class CurrencyExtensions
    {
        public static long ToCents(this CurrencyDto currency)
        {
            if (currency.Type == EnumCurrencyType.Money)
            {
                return (long) (currency.Amount.ToDecimal() * 100);
            }

            if (currency.Type == EnumCurrencyType.Token)
            {
                return (long) currency.Amount.ToDecimal();
            }

            throw new InvalidOperationException("Invalid currency type.");
        }
    }
}
