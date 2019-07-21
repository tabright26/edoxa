// Filename: ParticipantFaker.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    internal sealed class ParticipantFaker : Faker<Participant>
    {
        public ParticipantFaker(ChallengeGame game, DateTime createdAt, DateTime startedAt)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var participant = new Participant(
                        faker.User().Id(),
                        faker.Participant().GameAccountId(game),
                        new DateTimeProvider(FakerHub.Date.Between(createdAt, startedAt))
                    );

                    participant.SetEntityId(faker.Participant().Id());

                    return participant;
                }
            );
        }
    }
}
