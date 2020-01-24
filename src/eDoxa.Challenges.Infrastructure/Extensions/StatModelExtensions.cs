// Filename: StatModelExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class StatModelExtensions
    {
        public static Stat ToEntity(this StatModel model)
        {
            return new Stat(new StatName(model.Name), new StatValue(model.Value), new StatWeighting(model.Weighting));
        }
    }
}
