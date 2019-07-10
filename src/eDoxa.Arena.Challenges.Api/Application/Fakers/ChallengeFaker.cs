// Filename: ChallengeFaker.cs
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

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers
{
    public sealed class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null, Currency entryFeeCurrency = null)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var fakeGame = faker.Challenge().Game(game);

                    var fakeState = faker.Challenge().State(state);

                    var setup = faker.Challenge().Setup(entryFeeCurrency);

                    var scoring = new ScoringFactory().CreateInstance(fakeGame).Scoring;

                    var payout = new PayoutFactory().CreateInstance().GetPayout(setup.PayoutEntries, setup.EntryFee);

                    var duration = faker.Timeline().Duration();

                    var utcNow = DateTime.UtcNow.DateKeepHours();

                    var createdAt = faker.Date.Recent(1, utcNow);

                    var startedAt = faker.Date.Between(createdAt, utcNow);

                    var endedAt = startedAt + duration;

                    var closedAt = faker.Date.Soon(1, endedAt);

                    var synchronizedAt = faker.Date.Between(startedAt, closedAt);

                    var challenge = new Challenge(
                        faker.Challenge().Name(),
                        fakeGame,
                        setup,
                        new ChallengeTimeline(new DateTimeProvider(createdAt), duration),
                        scoring,
                        payout
                    );

                    challenge.SetEntityId(faker.Challenge().Id());

                    var participantFaker = new ParticipantFaker(fakeGame, createdAt, startedAt);

                    participantFaker.UseSeed(faker.Random.Int());

                    var participants = participantFaker.Generate(this.ParticipantCount(fakeState, setup.Entries));

                    participants.ForEach(participant => challenge.Register(participant));

                    if (fakeState != ChallengeState.Inscription)
                    {
                        challenge.Start(new DateTimeProvider(startedAt));

                        participants.ForEach(
                            participant =>
                            {
                                var matchFaker = new MatchFaker(fakeGame, scoring, synchronizedAt);

                                matchFaker.UseSeed(faker.Random.Int());

                                var matches = matchFaker.Generate(this.MatchCount(fakeState, setup.BestOf));

                                matches.ForEach(participant.Snapshot);
                            }
                        );

                        challenge.Synchronize(new DateTimeProvider(synchronizedAt));

                        if (fakeState == ChallengeState.Ended || fakeState == ChallengeState.Closed)
                        {
                            challenge.Start(new DateTimeProvider(startedAt - duration));
                        }

                        if (fakeState == ChallengeState.Closed)
                        {
                            challenge.Close(new DateTimeProvider(closedAt));
                        }
                    }

                    return challenge;
                }
            );
        }

        private int ParticipantCount(ChallengeState state, Entries entries)
        {
            return state == ChallengeState.Inscription ? new Entries(FakerHub.Random.Int(1, entries - 1)) : entries;
        }

        private int MatchCount(ChallengeState state, BestOf bestOf)
        {
            return state != ChallengeState.Inscription ? FakerHub.Random.Int(1, bestOf + 3) : 0;
        }

        [NotNull]
        public override IChallenge Generate(string ruleSets = null)
        {
            FakerExtensions.ResetUserIds();

            return base.Generate(ruleSets);
        }
    }
}
