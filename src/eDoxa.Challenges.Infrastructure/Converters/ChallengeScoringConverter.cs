// Filename: ChallengeScoringConverter.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain;
using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Infrastructure.Converters
{
    public sealed class ChallengeScoringConverter : ValueConverter<Option<IScoring>, string>
    {
        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.CreateMap<StatName, string>().ConvertUsing(name => name.ToString());
            config.CreateMap<StatWeighting, float>().ConvertUsing(weighting => Convert.ToSingle(weighting));
            config.CreateMap<IDictionary<string, float>, Scoring>().ConvertUsing(dictionary => new Scoring(dictionary));
        }));

        public ChallengeScoringConverter() : base(
            scoring => JsonConvert.SerializeObject(Mapper.Map<Dictionary<string, float>>(scoring.SingleOrDefault()), Formatting.None),
            scoring => scoring != null
                ? new Option<IScoring>(Mapper.Map<Scoring>(JsonConvert.DeserializeObject<Dictionary<string, float>>(scoring)))
                : new Option<IScoring>()
        )
        {
        }
    }
}