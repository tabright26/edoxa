// Filename: MatchListProfile.cs
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

using eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate;

namespace eDoxa.Challenges.DTO.Profiles
{
    internal sealed class MatchListProfile : Profile
    {
        public MatchListProfile()
        {
            this.CreateMap<IEnumerable<Match>, MatchListDTO>().ForMember(list => list.Items, config => config.MapFrom(matches => matches.ToList()));
        }
    }
}