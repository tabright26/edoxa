// Filename: PayoutFactory.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Strategies;

namespace eDoxa.Cashier.Api.Application.Factories
{
    public sealed class ChallengePayoutFactory : IChallengePayoutFactory
    {
        private readonly IChallengePayoutStrategy _strategy;

        public ChallengePayoutFactory(IChallengePayoutStrategy? strategy = null)
        {
            _strategy = strategy ?? new ChallengePayoutStrategy();
        }

        public IChallengePayoutStrategy CreateInstance()
        {
            return _strategy;
        }
    }
}
