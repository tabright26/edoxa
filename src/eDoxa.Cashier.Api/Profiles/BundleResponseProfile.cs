// Filename: BundleResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Profiles
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
