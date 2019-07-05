// Filename: IAccountService.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task DepositAsync(string customerId, UserId userId, ICurrency currency, CancellationToken cancellationToken = default);

        Task WithdrawalAsync(string connectAccountId, UserId userId, Money money, CancellationToken cancellationToken = default);
    }
}
