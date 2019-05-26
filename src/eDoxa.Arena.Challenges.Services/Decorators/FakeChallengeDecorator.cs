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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Arena.Challenges.Services.Decorators
{
    public sealed class FakeChallengeDecorator : Challenge
    {
        private static readonly Random Random = new Random();

        public FakeChallengeDecorator(Challenge challenge) : base(challenge.Game, challenge.Name, challenge.Setup, challenge.Duration, challenge.Scoring)
        {
            for (var index = 0; index < Setup.Entries; index++)
            {
                this.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));
            }

            Participants.ForEach(
                participant =>
                {
                    for (var index = 0; index < Random.Next(2, Setup.BestOf + 2); index++)
                    {
                        this.SnapshotParticipantMatch(participant.Id, FakeMatchStats());
                    }
                }
            );
        }

        private static IMatchStats FakeMatchStats()
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
