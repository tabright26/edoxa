// Filename: ChallengeTypeConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ChallengeTypeConverter : ITypeConverter<ChallengeModel, IChallenge>
    {
        [NotNull]
        public IChallenge Convert([NotNull] ChallengeModel source, [NotNull] IChallenge destination, [NotNull] ResolutionContext context)
        {
            var challenge = new Challenge(
                new ChallengeName(source.Name),
                ChallengeGame.FromValue(source.Game),
                context.Mapper.Map<ChallengeSetup>(source.Setup),
                new ChallengeTimeline(new PersistentDateTimeProvider(source.Timeline.CreatedAt), new ChallengeDuration(TimeSpan.FromTicks(source.Timeline.Duration))),
                context.Mapper.Map<IScoring>(source.ScoringItems),
                context.Mapper.Map<IPayout>(source.Buckets)
            );

            challenge.SetEntityId(ChallengeId.FromGuid(source.Id));

            var participants = context.Mapper.Map<ICollection<Participant>>(source.Participants);

            participants.ForEach(participant => challenge.Register(participant));

            if (source.Timeline.StartedAt.HasValue)
            {
                challenge.Start(new PersistentDateTimeProvider(source.Timeline.StartedAt.Value));
            }

            if (source.SynchronizedAt.HasValue)
            {
                challenge.Synchronize(new PersistentDateTimeProvider(source.SynchronizedAt.Value));
            }

            if (source.Timeline.ClosedAt.HasValue)
            {
                challenge.Close(new PersistentDateTimeProvider(source.Timeline.ClosedAt.Value));
            }

            return challenge;
        }
    }
}
