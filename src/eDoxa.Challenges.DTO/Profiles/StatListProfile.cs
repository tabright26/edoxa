﻿// Filename: StatListProfile.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    public sealed class StatListProfile : Profile
    {
        public StatListProfile()
        {
            this.CreateMap<IEnumerable<Stat>, StatListDTO>().ForMember(list => list.Items, config => config.MapFrom(stats => stats.ToList()));
        }
    }
}