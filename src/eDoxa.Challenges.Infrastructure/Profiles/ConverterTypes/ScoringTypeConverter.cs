// Filename: ScoringTypeConverter.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ScoringTypeConverter : ITypeConverter<ICollection<ScoringItemModel>, IScoring>
    {
        public IScoring Convert(ICollection<ScoringItemModel> source, IScoring destination, ResolutionContext context)
        {
            var scoring = new Scoring();

            foreach (var item in source)
            {
                scoring.Add(new StatName(item.Name), new StatWeighting(item.Weighting));
            }

            return scoring;
        }
    }
}
