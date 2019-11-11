// Filename: ScoringProfile.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
