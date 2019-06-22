// Filename: ChallengeFaker.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Fakers.Providers;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ChallengeFaker : Faker<Challenge>
    {
        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            this.CustomInstantiator(
                faker =>
                {
                    game = faker.Challenge().Game(game);

                    state = faker.Challenge().State(state);

                    var setup = faker.Challenge().Setup(entryFeeCurrency);

                    var scoring = ScoringFactory.Instance.CreateStrategy(game).Scoring;

                    var payout = PayoutFactory.Instance.CreateStrategy(setup.PayoutEntries, setup.EntryFee).Payout;

                    // -----------------------------------------

                    var timeline = faker.Challenge().Timeline(state);

                    var synchronizedAt = timeline != ChallengeState.Inscription && timeline.StartedAt.HasValue && timeline.EndedAt.HasValue
                        ? faker.Date.Between(timeline.StartedAt.Value, timeline.EndedAt.Value)
                        : (DateTime?) null;

                    var challenge = new Challenge(
                        faker.Challenge().Name(),
                        game,
                        setup,
                        timeline,
                        scoring,
                        payout
                    );

                    challenge.SetEntityId(faker.Challenge().Id());

                    var participantFaker = new ParticipantFaker(
                        game,
                        setup,
                        timeline,
                        scoring,
                        synchronizedAt
                    );

                    participantFaker.UseSeed(faker.Random.Int());

                    var participants =
                        participantFaker.Generate(state == ChallengeState.Inscription ? new Entries(faker.Random.Int(1, setup.Entries - 1)) : setup.Entries);

                    participants.ForEach(participant => challenge.Register(participant));

                    if (timeline.StartedAt.HasValue)
                    {
                        challenge.Start(new FakeDateTimeProvider(timeline.StartedAt.Value));
                    }

                    if (synchronizedAt.HasValue)
                    {
                        challenge.Synchronize(new FakeDateTimeProvider(synchronizedAt.Value));
                    }

                    if (timeline.ClosedAt.HasValue)
                    {
                        challenge.Close(new FakeDateTimeProvider(timeline.ClosedAt.Value));
                    }

                    return challenge;
                }
            );
        }
    }
}
