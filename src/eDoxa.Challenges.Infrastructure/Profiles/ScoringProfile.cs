// Filename: ScoringProfile.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes;

namespace eDoxa.Challenges.Infrastructure.Profiles
{
    internal sealed class ScoringProfile : Profile
    {
        public ScoringProfile()
        {
            this.CreateMap<ICollection<ScoringItemModel>, IScoring>().ConvertUsing(new ScoringTypeConverter());
        }
    }
}
