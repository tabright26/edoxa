// Filename: FakeChallengeDecorator.cs
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
        private readonly Random _random = new Random();

        public FakeChallengeDecorator(Challenge challenge) : base(
            challenge.Game,
            challenge.Name,
            challenge.Setup,
            challenge.Duration,
            challenge.Scoring,
            true
        )
        {
            for (var index = 0; index < Setup.Entries; index++)
            {
                this.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));
            }

            Participants.ForEach(
                participant =>
                {
                    for (var index = 0; index < _random.Next(2, Setup.BestOf + 2); index++)
                    {
                        this.SnapshotParticipantMatch(participant.Id, this.FakeMatchStats());
                    }
                }
            );
        }

        private IMatchStats FakeMatchStats()
        {
            return new MatchStats(
                new MatchExternalId(2233345251),
                new
                {
                    Kills = _random.Next(0, 40 + 1),
                    Deaths = _random.Next(0, 15 + 1),
                    Assists = _random.Next(0, 50 + 1),
                    TotalDamageDealtToChampions = _random.Next(10000, 500000 + 1),
                    TotalHeal = _random.Next(10000, 350000 + 1)
                }
            );
        }
    }
}
