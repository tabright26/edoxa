// Filename: DobExtensions.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Stripe;

namespace eDoxa.Payment.Domain.Stripe.Extensions
{
    public static class DatetimeExtensions
    {
        public static DobOptions ToStripe(this DateTime dob)
        {
            return new DobOptions
            {
                Year = dob.Year,
                Month = dob.Month,
                Day = dob.Day
            };
        }
    }
}
