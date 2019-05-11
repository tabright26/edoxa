// Filename: CustomerId.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;

namespace eDoxa.Cashier.Domain.Services.Stripe.Models
{
    [TypeConverter(typeof(StripeIdConverter))]
    public sealed class CustomerId : StripeId<CustomerId>
    {
        private const string Prefix = "cus";

        public CustomerId(string customerId) : base(customerId, Prefix)
        {
        }

        public static bool IsValid(string customerId)
        {
            return StripeId.IsValid(customerId, Prefix);
        }
    }
}