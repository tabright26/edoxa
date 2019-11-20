// Filename: ChallengeTypeConverter.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ChallengeTypeConverter : ITypeConverter<ChallengeModel, IChallenge>
    {
        public IChallenge Convert(ChallengeModel source, IChallenge destination, ResolutionContext context)
        {
            var challenge = new Challenge(
                ChallengeId.FromGuid(source.Id),
                new ChallengeName(source.Name),
                Game.FromValue(source.Game),
                new BestOf(source.BestOf),
                new Entries(source.Entries),
                new ChallengeTimeline(new DateTimeProvider(source.Timeline.CreatedAt), new ChallengeDuration(TimeSpan.FromTicks(source.Timeline.Duration))),
                context.Mapper.Map<IScoring>(source.ScoringItems));

            var participants = context.Mapper.Map<ICollection<Participant>>(source.Participants);

            foreach (var participant in participants)
            {
                challenge.Register(participant);
            }

            if (source.Timeline.StartedAt.HasValue)
            {
                challenge.Start(new DateTimeProvider(source.Timeline.StartedAt.Value));
            }

            if (source.SynchronizedAt.HasValue)
            {
                challenge.Synchronize(new DateTimeProvider(source.SynchronizedAt.Value));
            }

            if (source.Timeline.ClosedAt.HasValue)
            {
                challenge.Close(new DateTimeProvider(source.Timeline.ClosedAt.Value));
            }

            challenge.ClearDomainEvents();

            return challenge;
        }
    }
}
