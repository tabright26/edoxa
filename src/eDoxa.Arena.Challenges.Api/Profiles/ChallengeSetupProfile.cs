﻿// Filename: ChallengeSetupProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Profiles
{
    internal sealed class ChallengeSetupProfile : Profile
    {
        public ChallengeSetupProfile()
        {
            this.CreateMap<ChallengeSetup, ChallengeSetupViewModel>()
                .ForMember(setup => setup.BestOf, config => config.MapFrom<int>(setup => setup.BestOf))
                .ForMember(setup => setup.Entries, config => config.MapFrom<int>(setup => setup.Entries))
                .ForMember(setup => setup.PayoutEntries, config => config.MapFrom<int>(setup => setup.PayoutEntries))
                .ForMember(setup => setup.EntryFee, config => config.MapFrom(setup => setup.EntryFee));
        }
    }
}
