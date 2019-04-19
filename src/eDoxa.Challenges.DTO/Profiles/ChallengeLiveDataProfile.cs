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
                .ForMember(liveData => liveData.Entries, configuration => configuration.MapFrom(liveData => liveData.Entries.ToInt32()))
                .ForMember(liveData => liveData.PayoutEntries, configuration => configuration.MapFrom(liveData => liveData.PayoutEntries.ToInt32()))
                .ForMember(liveData => liveData.PrizePool, configuration => configuration.MapFrom(liveData => liveData.PrizePool.ToDecimal()))
                .ForMember(liveData => liveData.PrizeBreakdown, configuration => configuration.MapFrom(liveData => liveData.PrizeBreakdown));
        }
    }
}