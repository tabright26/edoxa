// Filename: ParticipantFaker.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers
{
    internal sealed class ParticipantFaker : Faker<Participant>
    {
        public ParticipantFaker(ChallengeGame game, DateTime createdAt, DateTime startedAt)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var participant = new Participant(
                        faker.UserId(),
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
