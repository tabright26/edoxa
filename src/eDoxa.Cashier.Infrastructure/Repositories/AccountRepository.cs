// Filename: AccountRepository.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

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

        private async Task<bool> AccountModelExistsAsync(Guid userId)
        {
            return await _context.Accounts.AsExpandable().AnyAsync(account => account.Id == userId);
        }

        private async Task<AccountModel?> FindUserAccountModelAsync(Guid userId)
        {
            var accountModels = from account in _context.Accounts.Include(account => account.Transactions).AsExpandable()
                                where account.Id == userId
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

        public async Task<IAccount> FindAccountAsync(UserId userId)
        {
            return await this.FindAccountOrNullAsync(userId) ?? throw new InvalidOperationException("Account does not exists.");
        }

        public async Task<bool> AccountExistsAsync(UserId userId)
        {
            return await this.AccountModelExistsAsync(userId);
        }

        public async Task<IAccount?> FindAccountOrNullAsync(UserId userId)
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
                _materializedIds[accountModel.Id] = account;
            }
        }

        private void CopyChanges(IAccount account, AccountModel accountModel)
        {
            accountModel.DomainEvents = account.DomainEvents.ToList();

            account.ClearDomainEvents();

            foreach (var transactionModel in accountModel.Transactions)
            {
                this.CopyChanges(account.Transactions.Single(transaction => transaction.Id == transactionModel.Id), transactionModel);
            }

            var transactions =
                account.Transactions.Where(transaction => accountModel.Transactions.All(transactionModel => transactionModel.Id != transaction.Id));

            foreach (var transaction in _mapper.Map<ICollection<TransactionModel>>(transactions))
            {
                accountModel.Transactions.Add(transaction);
            }
        }

        private void CopyChanges(ITransaction transaction, TransactionModel transactionModel)
        {
            transactionModel.DomainEvents = transaction.DomainEvents.ToList();

            transaction.ClearDomainEvents();

            transactionModel.Status = transaction.Status.Value;
        }
    }
}
