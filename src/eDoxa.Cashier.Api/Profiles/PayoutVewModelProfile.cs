// Filename: PayoutVewModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    public class PayoutVewModelProfile : Profile
    {
        public PayoutVewModelProfile()
        {
            this.CreateMap<IPayout, PayoutViewModel>()
                .ForMember(payout => payout.PrizePool, config => config.MapFrom(payout => payout.PrizePool))
                .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}
