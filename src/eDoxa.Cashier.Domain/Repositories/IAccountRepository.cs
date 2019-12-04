// Filename: IAccountRepository.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IAccountRepository
    {
        void Create(IAccount account);

        Task<IAccount?> FindAccountAsync(UserId userId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
