// Filename: ITokenAccountRepository.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface ITokenAccountRepository : IRepository<TokenAccount>
    {
        Task<TokenAccount> GetUserAccountAsync(UserId userId);

        Task<TokenAccount> GetTokenAccountAsNoTrackingAsync(UserId userId);
    }
}
