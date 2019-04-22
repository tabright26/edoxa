// Filename: ChallengeSettingsProfile.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class ChallengeSettingsProfile : Profile
    {
        public ChallengeSettingsProfile()
        {
            this.CreateMap<ChallengeSettings, ChallengeSettingsDTO>()
                .ForMember(settings => settings.BestOf, config => config.MapFrom<int>(settings => settings.BestOf))
                .ForMember(settings => settings.Entries, config => config.MapFrom<int>(settings => settings.Entries))
                .ForMember(settings => settings.PayoutEntries, config => config.MapFrom<int>(settings => settings.PayoutEntries))
                .ForMember(settings => settings.EntryFee, config => config.MapFrom<decimal>(settings => settings.EntryFee))
                .ForMember(settings => settings.PrizePool, config => config.MapFrom<decimal>(settings => settings.PrizePool));
        }
    }
}