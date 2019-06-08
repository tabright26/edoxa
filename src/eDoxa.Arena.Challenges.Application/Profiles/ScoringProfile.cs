// Filename: ScoringProfile.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.Application.Profiles
{
    internal sealed class ScoringProfile : Profile
    {
        public ScoringProfile()
        {
            this.CreateMap<Scoring, ScoringViewModel>().ConstructUsing(scoring => new ScoringViewModel(scoring));
        }
    }
}
