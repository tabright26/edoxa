// Filename: AccountConverter.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Infrastructure.Profiles.Converters
{
    internal sealed class AccountConverter : ITypeConverter<AccountModel, IAccount>
    {
        public IAccount Convert(AccountModel source, IAccount destination, ResolutionContext context)
        {
            return new Account(UserId.FromGuid(source.Id), context.Mapper.Map<ICollection<ITransaction>>(source.Transactions));
        }
    }
}
