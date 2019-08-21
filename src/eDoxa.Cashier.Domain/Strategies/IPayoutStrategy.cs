// Filename: IPayoutStrategy.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Domain.Strategies
{
    public interface IPayoutStrategy
    {
        Task<IPayout> GetPayoutAsync(PayoutEntries entries, EntryFee entryFee);
    }
}
