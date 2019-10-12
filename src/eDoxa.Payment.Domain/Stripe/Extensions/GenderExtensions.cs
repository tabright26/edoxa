// Filename: GenderExtensions.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Payment.Domain.Stripe.Extensions
{
    public static class GenderExtensions
    {
        public static string? ToStripe(this Gender gender)
        {
            return gender != Gender.Other ? gender.Name.ToLowerInvariant() : null;
        }
    }
}
