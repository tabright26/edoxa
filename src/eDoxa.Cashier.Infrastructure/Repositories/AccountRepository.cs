// Filename: AccountRepository.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class AccountRepository : Repository<IAccount, AccountModel>
    {
        public AccountRepository(CashierDbContext context)
        {
            UnitOfWork = context;
            Accounts = context.Set<AccountModel>();
        }

        private IUnitOfWork UnitOfWork { get; }

        private DbSet<AccountModel> Accounts { get; }

        private async Task<bool> AccountModelExistsAsync(Guid userId)
        {
            return await Accounts.AsExpandable().AnyAsync(account => account.Id == userId);
        }

        private async Task<AccountModel?> FindUserAccountModelAsync(Guid userId)
        {
            var accountModels = from account in Accounts.Include(account => account.Transactions).AsExpandable()
                                where account.Id == userId
                                select account;

            return await accountModels.SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountRepository : IAccountRepository
    {
        public void Create(IAccount account)
        {
            var accountModel = account.ToModel();

            Accounts.Add(accountModel);

            Mappings[account] = accountModel;
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
            var account = Mappings.Keys.SingleOrDefault(x => x.Id == userId);

            if (account != null)
            {
                return account;
            }

            var accountModel = await this.FindUserAccountModelAsync(userId);

            if (accountModel == null)
            {
                return null;
            }

            account = accountModel.ToEntity();

            Mappings[account] = accountModel;

            return account;
        }

        public override async Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default)
        {
            foreach (var (account, accountModel) in Mappings)
            {
                this.CopyChanges(account, accountModel);
            }

            await UnitOfWork.CommitAsync(publishDomainEvents, cancellationToken);
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

            foreach (var transaction in transactions.Select(transaction => transaction.ToModel()))
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
