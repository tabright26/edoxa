// Filename: IBundleService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IBundleService
    {
        IImmutableSet<Bundle> FetchDepositMoneyBundles();

        IImmutableSet<Bundle> FetchDepositTokenBundles();

        IImmutableSet<Bundle> FetchWithdrawalMoneyBundles();
    }
}
