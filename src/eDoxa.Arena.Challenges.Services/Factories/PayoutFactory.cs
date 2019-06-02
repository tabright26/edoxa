// Filename: PayoutFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class PayoutFactory : IPayoutFactory
    {
        private static readonly Lazy<PayoutFactory> Lazy = new Lazy<PayoutFactory>(() => new PayoutFactory());

        public static PayoutFactory Instance => Lazy.Value;

        public IPayoutStrategy CreateStrategy(IChallenge challenge)
        {
            return new PayoutStrategy(challenge.Setup.PayoutEntries, challenge.Setup.EntryFee);
        }
    }
}
