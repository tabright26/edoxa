// Filename: IUserRepository.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Create(UserId userId, string connectAccountId, string customerId);

        [ItemCanBeNull]
        Task<User> GetUserAsync(UserId userId);

        [ItemCanBeNull]
        Task<User> GetUserAsNoTrackingAsync(UserId userId);
    }
}
