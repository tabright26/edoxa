// Filename: IMoneyAccountService.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Abstactions;

namespace eDoxa.Cashier.Services.Abstractions
{
    public interface IAccountService
    {
        Task<ITransaction> DepositAsync(UserId userId, ICurrency currency, CancellationToken cancellationToken = default);

        Task<ITransaction> WithdrawAsync(UserId userId, Money money, CancellationToken cancellationToken = default);
    }
}
