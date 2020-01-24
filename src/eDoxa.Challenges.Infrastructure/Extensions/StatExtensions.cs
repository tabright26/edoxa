// Filename: StatExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class StatExtensions
    {
        public static StatModel ToModel(this Stat stat)
        {
            return new StatModel
            {
                Name = stat.Name,
                Value = stat.Value,
                Weighting = stat.Weighting
            };
        }
    }
}
