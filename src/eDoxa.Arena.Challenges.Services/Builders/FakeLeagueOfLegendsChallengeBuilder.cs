// Filename: FakeLeagueOfLegendsChallengeBuilder.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Services.Builders
{
    public sealed class FakeLeagueOfLegendsChallengeBuilder : IFakeChallengeBuilder
    {
        private static readonly Random Random = new Random();
        private static readonly ScoringFactory ScoringFactory = ScoringFactory.Instance;

        private readonly Challenge _challenge;

        public FakeLeagueOfLegendsChallengeBuilder(ChallengeName name, Game game, BestOf bestOf, PayoutEntries payoutEntries, EntryFee entryFee, bool equivalentCurrency = true)
        {
            var setup = new ChallengeSetup(bestOf, payoutEntries, entryFee, equivalentCurrency);

            _challenge = new Challenge(game, name, setup, new ChallengeDuration(), ScoringFactory.CreateScoringStrategy(game));
        }

        public Challenge Build()
        {
            return _challenge;
        }

        public void RegisterParticipants()
        {
            for (var index = 0; index < _challenge.Setup.Entries; index++)
            {
                _challenge.RegisterParticipant(new UserId(), new ParticipantExternalAccount(Guid.NewGuid()));
            }
        }

        public void SnapshotParticipantMatches()
        {
            _challenge.Participants.ForEach(
                participant =>
                {
                    for (var index = 0; index < Random.Next(2, _challenge.Setup.BestOf + 2); index++)
                    {
                        _challenge.SnapshotParticipantMatch(participant.Id, RandomMatchStats());
                    }
                }
            );
        }

        private static IMatchStats RandomMatchStats()
        {
            return new MatchStats(
                new MatchExternalId(2233345251),
                new
                {
                    Kills = Random.Next(0, 40 + 1),
                    Deaths = Random.Next(0, 15 + 1),
                    Assists = Random.Next(0, 50 + 1),
                    TotalDamageDealtToChampions = Random.Next(10000, 500000 + 1),
                    TotalHeal = Random.Next(10000, 350000 + 1)
                }
            );
        }
    }
}
