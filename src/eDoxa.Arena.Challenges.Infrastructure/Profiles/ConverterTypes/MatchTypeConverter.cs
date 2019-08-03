// Filename: MatchTypeConverter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class MatchTypeConverter : ITypeConverter<MatchModel, IMatch>
    {
        
        public IMatch Convert( MatchModel matchModel,  IMatch destination,  ResolutionContext context)
        {
            var stats = context.Mapper.Map<ICollection<Stat>>(matchModel.Stats);

            var match = Convert(matchModel, stats);

            match.SetEntityId(MatchId.FromGuid(matchModel.Id));

            return match;
        }

        private static IMatch Convert(MatchModel matchModel, ICollection<Stat> stats)
        {
            var synchronizedAt = new DateTimeProvider(matchModel.SynchronizedAt);

            if (stats.Count == 1)
            {
                var stat = stats.Single();

                var score = new GameScore(ChallengeGame.FromName(stat.Name)!, new decimal(stat.Value));

                return new GameMatch(score, matchModel.GameReference, synchronizedAt);
            }

            return new StatMatch(new Scoring(stats), new GameStats(stats), matchModel.GameReference, synchronizedAt);
        }
    }
}
