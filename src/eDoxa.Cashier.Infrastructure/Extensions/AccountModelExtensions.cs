// Filename: AccountModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class AccountModelExtensions
    {
        public static IAccount ToEntity(this AccountModel model)
        {
            var account = new Account(model.Id.ConvertTo<UserId>(), model.Transactions.Select(transaction => transaction.ToEntity()));

            account.ClearDomainEvents();

            return account;
        }
    }
}
