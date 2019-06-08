// Filename: PayoutFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed class PayoutFactory : IPayoutFactory
    {
        public IPayoutStrategy CreateStrategy(IChallenge challenge)
        {
            return new PayoutStrategy(challenge.Setup.PayoutEntries, challenge.Setup.EntryFee);
        }
    }
}
