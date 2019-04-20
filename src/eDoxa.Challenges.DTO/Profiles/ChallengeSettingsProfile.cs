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
                .ForMember(settings => settings.BestOf, configuration => configuration.MapFrom<int>(settings => settings.BestOf))
                .ForMember(settings => settings.Entries, configuration => configuration.MapFrom<int>(settings => settings.Entries))
                .ForMember(settings => settings.PayoutEntries, configuration => configuration.MapFrom<int>(settings => settings.PayoutEntries))
                .ForMember(settings => settings.EntryFee, configuration => configuration.MapFrom<decimal>(settings => settings.EntryFee))
                .ForMember(settings => settings.PrizePool, configuration => configuration.MapFrom<decimal>(settings => settings.PrizePool));
        }
    }
}