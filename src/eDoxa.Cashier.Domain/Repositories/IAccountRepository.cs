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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IAccountRepository
    {
        void Create(IAccount account);

        [ItemCanBeNull]
        Task<IAccount> FindUserAccountAsync(UserId userId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
