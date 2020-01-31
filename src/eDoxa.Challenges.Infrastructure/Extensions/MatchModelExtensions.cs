// Filename: MatchModelExtensions.cs
// Date Created: 2020-01-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Infrastructure.Extensions
{
    public static class MatchModelExtensions
    {
        public static IMatch ToEntity(this MatchModel model)
        {
            var match = new Match(
                model.GameUuid,
                new DateTimeProvider(model.GameStartedAt),
                TimeSpan.FromTicks(model.GameDuration),
                model.Stats.Select(stat => stat.ToEntity()),
                new DateTimeProvider(model.SynchronizedAt));

            match.SetEntityId(MatchId.FromGuid(model.Id));

            match.ClearDomainEvents();

            return match;
        }
    }
}
