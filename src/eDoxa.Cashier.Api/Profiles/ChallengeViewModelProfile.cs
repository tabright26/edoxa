// Filename: ChallengeViewModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class ChallengeViewModelProfile : Profile
    {
        public ChallengeViewModelProfile()
        {
            this.CreateMap<IChallenge, ChallengeViewModel>()
                .ForMember(challenge => challenge.EntryFee, config => config.MapFrom(challenge => challenge.EntryFee))
                .ForMember(challenge => challenge.Payout, config => config.MapFrom(challenge => challenge.Payout));
        }
    }
}
