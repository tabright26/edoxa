// Filename: PayoutResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class PayoutResponseProfile : Profile
    {
        public PayoutResponseProfile()
        {
            this.CreateMap<IPayout, PayoutResponse>()
                .ForMember(payout => payout.PrizePool, config => config.MapFrom(payout => payout.PrizePool))
                .ForMember(payout => payout.Buckets, config => config.MapFrom(payout => payout.Buckets));
        }
    }
}
