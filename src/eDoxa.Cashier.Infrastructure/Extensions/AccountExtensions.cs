// Filename: AccountExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class AccountExtensions
    {
        public static AccountModel ToModel(this IAccount model)
        {
            var account = new AccountModel
            {
                Id = model.Id,
                Transactions = model.Transactions.Select(transaction => transaction.ToModel()).ToList(),
                DomainEvents = model.DomainEvents.ToList()
            };

            foreach (var transaction in account.Transactions)
            {
                transaction.Account = account;
            }

            return account;
        }
    }
}
