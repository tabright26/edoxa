// Filename: StripeExtensions.cs
// Date Created: 2019-05-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Extensions
{
    public static class StripeExtensions
    {
        public static StripeList<T> ToStripeList<T>(this IEnumerable<T> enumerable)
        {
            return new StripeList<T>
            {
                Data = enumerable.ToList()
            };
        }
    }
}