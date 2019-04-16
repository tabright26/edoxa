// Filename: IUserRepository.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Create(User user);

        Task<User> FindAsync(UserId userId);

        Task<User> FindAsNoTrackingAsync(UserId userId);
    }
}