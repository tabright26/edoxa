// Filename: IMoneyAccountRepository.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IMoneyAccountRepository : IRepository<MoneyAccount>
    {
        Task<MoneyAccount> FindUserAccountAsync(UserId userId);
    }
}