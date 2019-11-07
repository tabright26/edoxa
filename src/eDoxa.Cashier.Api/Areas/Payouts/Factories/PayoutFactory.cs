// Filename: PayoutFactory.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Areas.Payouts.Strategies;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Strategies;

namespace eDoxa.Cashier.Api.Areas.Payouts.Factories
{
    public sealed class PayoutFactory : IPayoutFactory
    {
        private readonly IPayoutStrategy _strategy;

        public PayoutFactory(IPayoutStrategy? strategy = null)
        {
            _strategy = strategy ?? new PayoutStrategy();
        }

        public IPayoutStrategy CreateInstance()
        {
            return _strategy;
        }
    }
}
