// Filename: PrizePoolViewModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    public class PrizePoolViewModelProfile : Profile
    {
        public PrizePoolViewModelProfile()
        {
            this.CreateMap<PrizePool, PrizePoolViewModel>()
                .ForMember(prizePool => prizePool.Currency, config => config.MapFrom(prizePool => prizePool.Currency.Name))
                .ForMember(prizePool => prizePool.Amount, config => config.MapFrom(prizePool => prizePool.Amount));
        }
    }
}
