// Filename: CardId.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

namespace eDoxa.Cashier.Domain.Services.Stripe
{
    [TypeConverter(typeof(StripeIdTypeConverter))]
    public sealed class CardId : StripeId<CardId>
    {
        private const string Prefix = "card";

        public CardId() : base(Prefix)
        {
        }
    }
}