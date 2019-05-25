// Filename: ChallengeSetupProfile.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class ChallengeSetupProfile : Profile
    {
        public ChallengeSetupProfile()
        {
            this.CreateMap<ChallengeSetup, ChallengeSetupDTO>()
                .ForMember(setup => setup.BestOf, config => config.MapFrom<int>(setup => setup.BestOf))
                .ForMember(setup => setup.Entries, config => config.MapFrom<int>(setup => setup.Entries))
                .ForMember(setup => setup.PayoutEntries, config => config.MapFrom<int>(setup => setup.PayoutEntries))
                .ForMember(setup => setup.PrizePool, config => config.MapFrom<decimal>(setup => setup.PrizePool))
                .ForMember(setup => setup.EntryFee, config => config.MapFrom<decimal>(setup => setup.EntryFee))
                .ForMember(setup => setup.Currency, config => config.MapFrom(setup => setup.EntryFee.Currency));
        }
    }
}
