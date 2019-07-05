﻿// Filename: MatchProfile.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles
{
    internal sealed class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<MatchModel, Match>().ConvertUsing(new MatchTypeConverter());

            this.CreateMap<Match, MatchModel>()
                .ForMember(match => match.Id, config => config.MapFrom<Guid>(match => match.Id))
                .ForMember(match => match.SynchronizedAt, config => config.MapFrom(match => match.SynchronizedAt))
                .ForMember(match => match.GameReference, config => config.MapFrom<string>(match => match.GameReference))
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats))
                .ForMember(match => match.Participant, config => config.Ignore());
        }
    }
}