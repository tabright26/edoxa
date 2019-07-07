// Filename: ParticipantTypeConverter.cs
// Date Created: 2019-06-21
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
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class ParticipantTypeConverter : ITypeConverter<ParticipantModel, Participant>
    {
        [NotNull]
        public Participant Convert([NotNull] ParticipantModel source, [NotNull] Participant destination, [NotNull] ResolutionContext context)
        {
            var participant = new Participant(
                UserId.FromGuid(source.UserId),
                new GameAccountId(source.GameAccountId),
                new DateTimeProvider(source.RegisteredAt)
            );

            participant.SetEntityId(ParticipantId.FromGuid(source.Id));

            var matches = context.Mapper.Map<ICollection<Match>>(source.Matches);

            matches.ForEach(match => participant.Snapshot(match));

            return participant;
        }
    }
}
