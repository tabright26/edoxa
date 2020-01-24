// Filename: MatchExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class MatchExtensions
    {
        public static MatchModel ToModel(this IMatch match)
        {
            return new MatchModel
            {
                Id = match.Id,
                GameUuid = match.GameUuid,
                GameStartedAt = match.GameStartedAt,
                GameDuration = match.GameDuration.Ticks,
                SynchronizedAt = match.SynchronizedAt,
                Stats = match.Stats.Select(stat => stat.ToModel()).ToList()
            };
        }
    }
}
