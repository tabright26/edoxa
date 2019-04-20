// Filename: ChallengeLiveDataProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengeLiveDataProfile : Profile
    {
        public ChallengeLiveDataProfile()
        {
            this.CreateMap<ChallengeLiveData, ChallengeLiveDataDTO>()
                .ForMember(liveData => liveData.Entries, configuration => configuration.MapFrom<int>(liveData => liveData.Entries))
                .ForMember(liveData => liveData.PayoutEntries, configuration => configuration.MapFrom<int>(liveData => liveData.PayoutEntries))
                .ForMember(liveData => liveData.PrizePool, configuration => configuration.MapFrom<decimal>(liveData => liveData.PrizePool))
                .ForMember(liveData => liveData.Payout, configuration => configuration.MapFrom(liveData => liveData.Payout));
        }
    }
}