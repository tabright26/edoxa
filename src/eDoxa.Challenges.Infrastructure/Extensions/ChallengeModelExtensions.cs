// Filename: ChallengeModelExtensions.cs
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
    public static class ChallengeModelExtensions
    {
        public static IChallenge ToEntity(this ChallengeModel model)
        {
            var challenge = new Challenge(
                ChallengeId.FromGuid(model.Id),
                new ChallengeName(model.Name),
                Game.FromValue(model.Game),
                new BestOf(model.BestOf),
                new Entries(model.Entries),
                new ChallengeTimeline(new DateTimeProvider(model.Timeline.CreatedAt), new ChallengeDuration(TimeSpan.FromTicks(model.Timeline.Duration))),
                model.ScoringItems.ToEntity());

            foreach (var participant in model.Participants.Select(participant => participant.ToEntity()))
            {
                challenge.Register(participant);
            }

            if (model.Timeline.StartedAt.HasValue)
            {
                challenge.Start(new DateTimeProvider(model.Timeline.StartedAt.Value));
            }

            if (model.SynchronizedAt.HasValue)
            {
                challenge.Synchronize(new DateTimeProvider(model.SynchronizedAt.Value));
            }

            if (model.Timeline.ClosedAt.HasValue)
            {
                challenge.Close(new DateTimeProvider(model.Timeline.ClosedAt.Value));
            }

            challenge.ClearDomainEvents();

            return challenge;
        }
    }
}
