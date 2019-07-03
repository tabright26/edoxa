// Filename: UserQuery.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Api.Infrastructure.Queries
{
    public sealed partial class UserQuery
    {
        public UserQuery(CashierDbContext cashierDbContext, IMapper mapper)
        {
            Mapper = mapper;
            Users = cashierDbContext.Users.AsNoTracking();
        }

        public IMapper Mapper { get; }

        private IQueryable<UserModel> Users { get; }

        private async Task<UserModel> FindUserModelAsync(Guid userId)
        {
            var users = from user in Users.AsExpandable()
                        where user.Id == userId
                        select user;

            return await users.SingleOrDefaultAsync();
        }
    }

    public sealed partial class UserQuery : IUserQuery
    {
        [ItemCanBeNull]
        public async Task<User> FindUserAsync(UserId userId)
        {
            var userModel = await this.FindUserModelAsync(userId);

            return Mapper.Map<User>(userModel);
        }
    }
}
