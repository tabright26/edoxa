// Filename: StripeCardId.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Cashier.Domain.Abstractions;

namespace eDoxa.Cashier.Domain.AggregateModels
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
