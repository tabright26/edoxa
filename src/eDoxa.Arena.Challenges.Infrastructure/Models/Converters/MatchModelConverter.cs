﻿// Filename: MapMatchConverter.cs
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
using eDoxa.Seedwork.Infrastructure;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Models.Converters
{
    internal sealed class MatchModelConverter : ITypeConverter<MatchModel, Match>
    {
        [NotNull]
        public Match Convert([NotNull] MatchModel source, [NotNull] Match destination, [NotNull] ResolutionContext context)
        {
            var match = new PersistentMatch(MatchId.FromGuid(source.Id), source.GameMatchId, new PersistentDateTimeProvider(source.SynchronizedAt));

            var stats = context.Mapper.Map<ICollection<Stat>>(source.Stats);

            var scoring = new Scoring(stats);

            var matchStats = new MatchStats(stats);

            match.SnapshotStats(scoring, matchStats);

            return match;
        }
    }
}
