// Filename: ScoringTypeConverter.cs
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ScoringTypeConverter : ITypeConverter<ICollection<ScoringItemModel>, IScoring>
    {
        
        public IScoring Convert( ICollection<ScoringItemModel> source,  IScoring destination,  ResolutionContext context)
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
