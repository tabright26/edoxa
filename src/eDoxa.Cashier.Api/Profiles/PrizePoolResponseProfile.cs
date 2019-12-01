// Filename: PrizePoolResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class PrizePoolResponseProfile : Profile
    {
        public PrizePoolResponseProfile()
        {
            this.CreateMap<PrizePool, PrizePoolResponse>()
                .ForMember(prizePool => prizePool.Currency, config => config.MapFrom(prizePool => prizePool.Currency.Name))
                .ForMember(prizePool => prizePool.Amount, config => config.MapFrom(prizePool => prizePool.Amount));
        }
    }
}
