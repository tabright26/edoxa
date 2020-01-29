// Filename: IPayoutStrategy.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Domain.Strategies
{
    public interface IChallengePayoutStrategy
    {
        IChallengePayout GetPayout(ChallengePayoutEntries entries, EntryFee entryFee);
    }
}
