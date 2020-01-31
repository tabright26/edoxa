// Filename: ScoringItemModelExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ScoringItemModelExtensions // TODO Refactoring
    {
        public static Scoring ToEntity(this IEnumerable<ScoringItemModel> models)
        {
            var scoring = new Scoring();

            foreach (var item in models.OrderBy(item => item.Order).ToList())
            {
                scoring.Add(new StatName(item.Name), new StatWeighting(item.Weighting));
            }

            return scoring;
        }
    }
}
