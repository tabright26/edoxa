﻿// Filename: AccountRepository.cs
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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class AccountRepository
    {
        private readonly IDictionary<Guid, IAccount> _materializedIds = new Dictionary<Guid, IAccount>();
        private readonly IDictionary<IAccount, AccountModel> _materializedObjects = new Dictionary<IAccount, AccountModel>();
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        private async Task<AccountModel> FindUserAccountModelAsync(Guid userId)
        {
            var accounts = from account in _context.Accounts.Include(account => account.User).Include(account => account.Transactions).AsExpandable()
                           where account.User.Id == userId
                           select account;

            return await accounts.SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountRepository : IAccountRepository
    {
        [ItemCanBeNull]
        public async Task<IAccount> FindUserAccountAsync(UserId userId)
        {
            if (_materializedIds.TryGetValue(userId, out var account))
            {
                return account;
            }

            var accountModel = await this.FindUserAccountModelAsync(userId);

            if (accountModel == null)
            {
                return null;
            }

            account = _mapper.Map<IAccount>(accountModel);

            _materializedObjects[account] = accountModel;

            _materializedIds[userId] = account;

            return account;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var (account, accountModel) in _materializedObjects)
            {
                this.CopyChanges(account, accountModel);
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var (account, accountModel) in _materializedObjects)
            {
                _materializedIds[accountModel.User.Id] = account;
            }
        }

        private void CopyChanges(IAccount account, AccountModel accountModel)
        {
            var transactions = account.Transactions.Where(transaction => accountModel.Transactions.All(transactionModel => transactionModel.Id != transaction.Id));

            _mapper.Map<ICollection<TransactionModel>>(transactions).ForEach(transaction => accountModel.Transactions.Add(transaction));
        }
    }
}
