// Filename: ScoringItemExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class ScoringItemExtensions // TODO Refactoring
    {
        public static ICollection<ScoringItemModel> ToModel(this IScoring scoring)
        {
            return scoring.Select(
                    (x, index) => new ScoringItemModel
                    {
                        Name = x.Key,
                        Weighting = x.Value,
                        Order = index
                    })
                .ToList();
        }
    }
}
