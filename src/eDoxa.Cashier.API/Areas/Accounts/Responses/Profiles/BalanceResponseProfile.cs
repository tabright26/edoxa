// Filename: BalanceResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Areas.Accounts.Responses.Profiles
{
    internal sealed class BalanceResponseProfile : Profile
    {
        public BalanceResponseProfile()
        {
            this.CreateMap<Balance, BalanceResponse>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}
