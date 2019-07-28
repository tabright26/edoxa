﻿// Filename: ParticipantDataSet.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
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
            return state != ChallengeState.Inscription ? Faker.Date.Recent(1, startedAt) : Faker.Date.Soon(1, DateTime.UtcNow.Date);
        }

        public GameAccountId GameAccountId(ChallengeGame game)
        {
            if (game == ChallengeGame.LeagueOfLegends)
            {
                return new GameAccountId(Faker.Random.Replace("*******_******************_*_*****************"));
            }

            throw new ArgumentNullException(nameof(game));
        }
    }
}