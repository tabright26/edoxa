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

using FluentValidation.Results;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task<ValidationResult> DepositAsync(IAccount account, ICurrency currency, string customerId, CancellationToken cancellationToken = default);

        Task<ValidationResult> WithdrawalAsync(IMoneyAccount account, ICurrency currency, string connectAccountId, CancellationToken cancellationToken = default);

        Task<IAccount?> FindUserAccountAsync(UserId userId);
    }
}
