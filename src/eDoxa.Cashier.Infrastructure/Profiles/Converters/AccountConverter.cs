// Filename: AccountConverter.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class AccountConverter : ITypeConverter<AccountModel, IAccount>
    {
        [NotNull]
        public IAccount Convert([NotNull] AccountModel source, [NotNull] IAccount destination, [NotNull] ResolutionContext context)
        {
            var account = new Account(UserId.FromGuid(source.UserId));

            account.SetEntityId(AccountId.FromGuid(source.Id));

            var transactions = context.Mapper.Map<ICollection<ITransaction>>(source.Transactions);

            transactions.ForEach(transaction => account.CreateTransaction(transaction));

            return account;
        }
    }
}
