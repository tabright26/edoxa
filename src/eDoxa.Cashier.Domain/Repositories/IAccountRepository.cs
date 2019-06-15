// Filename: IAccountRepository.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountAsync(UserId userId);

        Task<Account> GetAccountAsNoTrackingAsync(UserId userId);

        Task<Balance> GetBalanceAsNoTrackingAsync(UserId userId, CurrencyType currency);

        Task<IReadOnlyCollection<Transaction>> GetTransactionsAsNoTrackingAsync(UserId userId);
    }
}
