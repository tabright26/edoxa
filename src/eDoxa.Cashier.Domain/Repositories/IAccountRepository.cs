// Filename: IMoneyAccountRepository.cs
// Date Created: 2019-05-06
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
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountAsync(UserId userId);

        Task<Balance> GetBalanceAsNoTrackingAsync(UserId userId, CurrencyType currency);

        Task<IReadOnlyCollection<Transaction>> GetTransactionsAsNoTrackingAsync(UserId userId);
    }
}
