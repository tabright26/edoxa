// Filename: MatchListProfile.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.DTO.Profiles
{
    internal sealed class MatchListProfile : Profile
    {
        public MatchListProfile()
        {
            this.CreateMap<IEnumerable<Match>, MatchListDTO>().ForMember(list => list.Items, config => config.MapFrom(matches => matches.ToList()));
        }
    }
}
