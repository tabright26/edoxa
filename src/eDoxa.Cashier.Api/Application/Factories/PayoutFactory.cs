// Filename: PayoutFactory.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Strategies;

namespace eDoxa.Cashier.Api.Application.Factories
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
