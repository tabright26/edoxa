// Filename: MatchTypeConverter.cs
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

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class MatchTypeConverter : ITypeConverter<MatchModel, IMatch>
    {
        [NotNull]
        public IMatch Convert([NotNull] MatchModel source, [NotNull] IMatch destination, [NotNull] ResolutionContext context)
        {
            var stats = context.Mapper.Map<ICollection<Stat>>(source.Stats);

            var match = new StatMatch(stats, source.GameReference, new DateTimeProvider(source.SynchronizedAt));

            match.SetEntityId(MatchId.FromGuid(source.Id));

            return match;
        }
    }
}
