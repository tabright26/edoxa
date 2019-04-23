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
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Infrastructure.Converters
{
    public sealed class ChallengeScoringConverter : ValueConverter<Maybe<IChallengeScoring>, string>
    {
        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(config =>
        {
            config.CreateMap<StatName, string>().ConvertUsing(name => name.ToString());
            config.CreateMap<StatWeighting, float>().ConvertUsing(weighting => Convert.ToSingle(weighting));
            config.CreateMap<IDictionary<string, float>, ChallengeScoring>().ConvertUsing(dictionary => new ChallengeScoring(dictionary));
        }));

        public ChallengeScoringConverter() : base(
            scoring => JsonConvert.SerializeObject(Mapper.Map<Dictionary<string, float>>(scoring.SingleOrDefault()), Formatting.None),
            scoring => scoring != null
                ? new Maybe<IChallengeScoring>(Mapper.Map<ChallengeScoring>(JsonConvert.DeserializeObject<Dictionary<string, float>>(scoring)))
                : new Maybe<IChallengeScoring>()
        )
        {
        }
    }
}