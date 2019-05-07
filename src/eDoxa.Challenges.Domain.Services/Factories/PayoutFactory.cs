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

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default.Strategies;

namespace eDoxa.Challenges.Domain.Services.Factories
{
    public sealed class PayoutFactory
    {
        private static readonly Lazy<PayoutFactory> Lazy = new Lazy<PayoutFactory>(() => new PayoutFactory());

        public static PayoutFactory Instance => Lazy.Value;

        public IPayoutStrategy CreatePayout(Challenge challenge)
        {
            switch (challenge.Setup.Type)
            {
                case ChallengeType.Default:

                    return new DefaultPayoutStrategy();

                default:

                    throw new NotImplementedException();
            }
        }
    }
}