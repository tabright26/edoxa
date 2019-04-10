// Filename: IUserRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Notifications.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Create(User user);

        Task<User> FindAsync(UserId userId);
    }
}