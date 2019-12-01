// Filename: ParticipantDataSet.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.DataSets
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

        public PlayerId PlayerId(Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return Seedwork.Domain.Misc.PlayerId.Parse(Faker.Random.Replace("*******_******************_*_*****************"));
            }

            throw new ArgumentNullException(nameof(game));
        }
    }
}
