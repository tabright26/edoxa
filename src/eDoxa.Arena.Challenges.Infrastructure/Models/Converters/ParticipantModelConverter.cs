﻿// Filename: MapParticipantConverter.cs
// Date Created: 2019-06-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Models.Converters
{
    internal sealed class ParticipantModelConverter : ITypeConverter<ParticipantModel, Participant>
    {
        [NotNull]
        public Participant Convert([NotNull] ParticipantModel source, [NotNull] Participant destination, [NotNull] ResolutionContext context)
        {
            var participant = new PersistentParticipant(
                ParticipantId.FromGuid(source.Id),
                UserId.FromGuid(source.UserId),
                new GameAccountId(source.GameAccountId),
                new PersistentDateTimeProvider(source.RegisteredAt)
            );

            var matches = context.Mapper.Map<ICollection<Match>>(source.Matches);

            matches.ForEach(match => participant.Synchronize(match));

            return participant;
        }
    }
}
