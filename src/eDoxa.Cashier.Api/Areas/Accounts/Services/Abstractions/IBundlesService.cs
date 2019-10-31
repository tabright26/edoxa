// Filename: IMoneyAccountService.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Immutable;

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions
{
    public interface IBundlesService
    {
        IImmutableSet<Bundle> FetchDepositMoneyBundles();

        IImmutableSet<Bundle> FetchDepositTokenBundles();

        IImmutableSet<Bundle> FetchWithdrawalMoneyBundles();
    }
}
