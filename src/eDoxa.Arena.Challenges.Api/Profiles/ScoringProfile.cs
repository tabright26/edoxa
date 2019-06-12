﻿// Filename: ScoringProfile.cs
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
    internal sealed class ScoringProfile : Profile
    {
        public ScoringProfile()
        {
            this.CreateMap<Scoring, ScoringViewModel>().ConstructUsing(scoring => new ScoringViewModel(scoring));
        }
    }
}
