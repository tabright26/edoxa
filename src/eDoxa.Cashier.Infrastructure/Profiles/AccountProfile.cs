// Filename: AccountProfile.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<AccountModel, IAccount>().ConvertUsing(new AccountConverter());
        }
    }
}
