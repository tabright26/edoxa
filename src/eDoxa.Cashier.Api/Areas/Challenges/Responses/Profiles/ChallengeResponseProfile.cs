// Filename: ChallengeResponseProfile.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses.Profiles
{
    internal sealed class ChallengeResponseProfile : Profile
    {
        public ChallengeResponseProfile()
        {
            this.CreateMap<IChallenge, ChallengeResponse>()
                .ForMember(challenge => challenge.EntryFee, config => config.MapFrom(challenge => challenge.EntryFee))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => challenge.Payout));
        }
    }
}
