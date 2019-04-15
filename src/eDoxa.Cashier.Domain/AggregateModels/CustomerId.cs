// Filename: CustomerId.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Stripe;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    [TypeConverter(typeof(StripeIdTypeConverter))]
    public sealed class CustomerId : StripeId<CustomerId>
    {
        private const string Prefix = "cus";

        public CustomerId() : base(Prefix)
        {
        }
    }
}