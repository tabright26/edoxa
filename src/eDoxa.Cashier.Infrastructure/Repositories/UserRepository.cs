// Filename: UserRepository.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        private readonly IDictionary<Guid, User> _materializedIds = new Dictionary<Guid, User>();
        private readonly IDictionary<User, UserModel> _materializedObjects = new Dictionary<User, UserModel>();
        private readonly IMapper _mapper;
        private readonly CashierDbContext _context;

        public UserRepository(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        public async Task<UserModel> FindUserModelAsync(Guid userId)
        {
            var users = from user in _context.Users.AsExpandable()
                        where user.Id == userId
                        select user;

            return await users.SingleOrDefaultAsync();
        }
    }

    public sealed partial class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            var userModel = _mapper.Map<UserModel>(user);

            userModel.Account = new AccountModel
            {
                Id = Guid.NewGuid(),
                Transactions = new List<TransactionModel>(),
                User = userModel
            };

            _context.Users.Add(userModel);

            _materializedObjects[user] = userModel;
        }

        [ItemCanBeNull]
        public async Task<User> FindUserAsync(UserId userId)
        {
            if (_materializedIds.TryGetValue(userId, out var user))
            {
                return user;
            }

            var userModel = await this.FindUserModelAsync(userId);

            if (userModel == null)
            {
                return null;
            }

            user = _mapper.Map<User>(userModel);

            _materializedObjects[user] = userModel;

            _materializedIds[userId] = user;

            return user;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var (user, userModel) in _materializedObjects)
            {
                this.CopyChanges(user, userModel);
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var (user, userModel) in _materializedObjects)
            {
                _materializedIds[userModel.Id] = user;
            }
        }

        public void CopyChanges(User user, UserModel userModel)
        {
            userModel.BankAccountId = user.BankAccountId;
        }
    }
}
