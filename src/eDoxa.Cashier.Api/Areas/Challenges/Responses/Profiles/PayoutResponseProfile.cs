// Filename: PayoutResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses.Profiles
{
    public class PayoutResponseProfile : Profile
    {
        public PayoutResponseProfile()
        {
            this.CreateMap<IPayout, PayoutResponse>()
                .ForMember(payout => payout.PrizePool, config => config.MapFrom(payout => payout.PrizePool))
                .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}
