// Filename: BundleResponseProfile.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Api.Areas.Accounts.Responses.Profiles
{
    internal sealed class BundleResponseProfile : Profile
    {
        public BundleResponseProfile()
        {
            this.CreateMap<Bundle, BundleResponse>()
                .ForMember(balance => balance.Amount, config => config.MapFrom(balance => balance.Currency.Amount))
                .ForMember(balance => balance.Price, config => config.MapFrom(balance => balance.Price.Money.Amount));
        }
    }
}
