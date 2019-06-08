// Filename: StripeCardId.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Stripe.Abstractions;

namespace eDoxa.Stripe.Models
{
    [TypeConverter(typeof(StripeIdConverter))]
    public sealed class StripeCardId : StripeId<StripeCardId>
    {
        private const string Prefix = "card";

        public StripeCardId(string cardId) : base(cardId, Prefix)
        {
        }
    }
}
