// Filename: ParticipantDataSet.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.DataSets
{
    public class ParticipantDataSet
    {
        public ParticipantDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public ParticipantId Id()
        {
            return ParticipantId.FromGuid(Faker.Random.Guid());
        }

        public DateTime RegisteredAt(ChallengeState state, DateTime? startedAt)
        {
            return state != ChallengeState.Inscription ? Faker.Date.Recent(1, startedAt) : Faker.Date.Soon(1, DateTime.UtcNow.DateKeepHours());
        }

        public GameAccountId GameAccountId(ChallengeGame game)
        {
            if (game == ChallengeGame.LeagueOfLegends)
            {
                return new GameAccountId(Faker.Random.Replace("*****_*************************"));
            }

            throw new ArgumentNullException(nameof(game));
        }

        public IEnumerable<MatchModel> Matches(
            ParticipantModel participantModel,
            ChallengeGame game,
            ChallengeState state,
            BestOf bestOf,
            DateTime? startedAt
        )
        {
            for (var index = 0; index < this.MatchCount(bestOf, state); index++)
            {
                yield return this.Match(participantModel, game, startedAt);
            }
        }

        private int MatchCount(BestOf bestOf, ChallengeState state)
        {
            return state != ChallengeState.Inscription ? Faker.Random.Int(1, bestOf + 3) : 0;
        }

        public MatchModel Match(ParticipantModel participantModel, ChallengeGame game, DateTime? startedAt)
        {
            return new MatchModel
            {
                Id = Faker.Match().Id(),
                GameMatchId = Faker.Match().GameId(game),
                SynchronizedAt = Faker.Match().SynchronizedAt(startedAt),
                Stats = this.Stats(participantModel.Challenge.ScoringItems, game).ToList(),
                Participant = participantModel
            };
        }

        public IEnumerable<StatModel> Stats(ICollection<ScoringItemModel> scoringItemModels, ChallengeGame game)
        {
            var stats = Faker.Match().Stats(game);

            for (var index = 0; index < scoringItemModels.Count; index++)
            {
                var item = scoringItemModels.ElementAt(index);

                var name = item.Name;

                if (!stats.ContainsKey(new StatName(name)))
                {
                    continue;
                }

                var value = stats[new StatName(name)];

                var weighting = item.Weighting;

                yield return new StatModel
                {
                    Name = name,
                    Value = value,
                    Weighting = weighting
                };
            }
        }
    }
}
