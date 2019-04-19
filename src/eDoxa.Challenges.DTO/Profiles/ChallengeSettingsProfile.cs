// Filename: ChallengeSettingsProfile.cs
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
    internal sealed class ChallengeSettingsProfile : Profile
    {
        public ChallengeSettingsProfile()
        {
            this.CreateMap<ChallengeSettings, ChallengeSettingsDTO>()
                .ForMember(settings => settings.BestOf, configuration => configuration.MapFrom(settings => settings.BestOf.ToInt32()))
                .ForMember(settings => settings.Entries, configuration => configuration.MapFrom(settings => settings.Entries.ToInt32()))
                .ForMember(settings => settings.PayoutEntries, configuration => configuration.MapFrom(settings => settings.PayoutEntries.ToInt32()))
                .ForMember(settings => settings.EntryFee, configuration => configuration.MapFrom(settings => settings.EntryFee.ToDecimal()))
                .ForMember(settings => settings.PrizePool, configuration => configuration.MapFrom(settings => settings.PrizePool.ToDecimal()));
        }
    }
}