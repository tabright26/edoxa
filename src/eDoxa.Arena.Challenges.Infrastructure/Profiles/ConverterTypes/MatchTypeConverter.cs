// Filename: MatchTypeConverter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Aggregate;

using IdentityServer4.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class MatchTypeConverter : ITypeConverter<MatchModel, IMatch>
    {
        [NotNull]
        public IMatch Convert([NotNull] MatchModel source, [NotNull] IMatch destination, [NotNull] ResolutionContext context)
        {
            var stats = context.Mapper.Map<ICollection<Stat>>(source.Stats);

            var match = Convert(source, stats);

            match.SetEntityId(MatchId.FromGuid(source.Id));

            return match;
        }

        private static IMatch Convert(MatchModel source, ICollection<Stat> stats)
        {
            var synchronizedAt = new DateTimeProvider(source.SynchronizedAt);

            if (stats.IsNullOrEmpty())
            {
                return new GameMatch(new GameScore(source.TotalScore), source.GameReference, synchronizedAt);
            }

            return new StatMatch(stats, source.GameReference, synchronizedAt);
        }
    }
}
