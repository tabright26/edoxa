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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Challenges.Infrastructure.Profiles
{
    internal sealed class MatchProfile : Profile
    {
        public MatchProfile()
        {
            this.CreateMap<MatchModel, IMatch>().ConvertUsing(new MatchTypeConverter());

            this.CreateMap<IMatch, MatchModel>()
                .ForMember(match => match.Id, config => config.MapFrom<Guid>(match => match.Id))
                .ForMember(match => match.GameUuid, config => config.MapFrom<string>(match => match.GameUuid))
                .ForMember(match => match.Stats, config => config.MapFrom(match => match.Stats))
                .ForMember(match => match.Participant, config => config.Ignore());
        }
    }
}
