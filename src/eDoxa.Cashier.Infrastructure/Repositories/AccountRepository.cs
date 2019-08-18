// Filename: AccountRepository.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;

using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        private async Task<AccountModel?> FindUserAccountModelAsync(Guid userId)
        {
            var accountModels = from account in _context.Accounts.Include(account => account.Transactions).AsExpandable()
                                where account.UserId == userId
                                select account;

            return await accountModels.SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountRepository : IAccountRepository
    {
        public void Create(IAccount account)
        {
            var accountModel = _mapper.Map<AccountModel>(account);

            _context.Accounts.Add(accountModel);

            _materializedObjects[account] = accountModel;
        }

        public async Task<IAccount?> FindUserAccountAsync(UserId userId)
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
                _materializedIds[accountModel.UserId] = account;
            }
        }

        private void CopyChanges(IAccount account, AccountModel accountModel)
        {
            var transactions =
                account.Transactions.Where(transaction => accountModel.Transactions.All(transactionModel => transactionModel.Id != transaction.Id));

            foreach (var transaction in _mapper.Map<ICollection<TransactionModel>>(transactions))
            {
                accountModel.Transactions.Add(transaction);
            }
        }
    }
}
