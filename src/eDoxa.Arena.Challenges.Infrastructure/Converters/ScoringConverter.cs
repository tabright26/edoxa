// Filename: ChallengeScoringConverter.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    public sealed class ScoringConverter : ValueConverter<IScoring, string>
    {
        private static readonly IMapper Mapper = new Mapper(
            new MapperConfiguration(
                config =>
                {
                    config.CreateMap<StatName, string>().ConvertUsing(name => name.ToString());
                    config.CreateMap<StatWeighting, float>().ConvertUsing(weighting => Convert.ToSingle(weighting));
                    config.CreateMap<IDictionary<string, float>, Scoring>().ConvertUsing(dictionary => new Scoring(dictionary));
                }
            )
        );

        public ScoringConverter() : base(
            scoring => JsonConvert.SerializeObject(Mapper.Map<Dictionary<string, float>>(scoring), Formatting.None),
            scoring => Mapper.Map<Scoring>(JsonConvert.DeserializeObject<Dictionary<string, float>>(scoring))
        )
        {
        }
    }
}
