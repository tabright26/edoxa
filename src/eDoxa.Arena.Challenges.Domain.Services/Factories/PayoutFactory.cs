// Filename: PayoutFactory.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Services.Strategies;

namespace eDoxa.Arena.Challenges.Domain.Services.Factories
{
    public sealed class PayoutFactory
    {
        private static readonly Lazy<PayoutFactory> Lazy = new Lazy<PayoutFactory>(() => new PayoutFactory());

        public static PayoutFactory Instance => Lazy.Value;

        public IPayoutStrategy CreatePayout()
        {
            return new PayoutStrategy();
        }
    }
}