// Filename: ChallengeFaker.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Providers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public sealed class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var fakeGame = faker.Challenge().Game(game);

                    var fakeState = faker.Challenge().State(state);

                    var scoring = new ScoringFactory().CreateInstance(fakeGame).Scoring;

                    var duration = faker.Challenge().Duration();

                    var utcNowDate = DateTime.UtcNow.Date;

                    var createdAt = faker.Date.Recent(1, utcNowDate);

                    var startedAt = faker.Date.Between(createdAt, utcNowDate);

                    var endedAt = startedAt + duration;

                    var closedAt = faker.Date.Soon(1, endedAt);

                    var synchronizedAt = faker.Date.Between(startedAt, closedAt);

                    var challenge = new Challenge(
                        faker.Challenge().Name(),
                        fakeGame,
                        faker.Challenge().BestOf(),
                        faker.Challenge().Entries(),
                        new ChallengeTimeline(new DateTimeProvider(createdAt), duration),
                        scoring
                    );

                    challenge.SetEntityId(faker.Challenge().Id());

                    var participantFaker = new ParticipantFaker(fakeGame, createdAt, startedAt);

                    participantFaker.UseSeed(faker.Random.Int());

                    var participants = participantFaker.Generate(this.ParticipantCount(fakeState, challenge.Entries));

                    participants.ForEach(participant => challenge.Register(participant));

                    if (fakeState != ChallengeState.Inscription)
                    {
                        challenge.Start(new DateTimeProvider(startedAt));

                        participants.ForEach(
                            participant =>
                            {
                                var matchFaker = new MatchFaker(fakeGame, scoring, synchronizedAt);

                                matchFaker.UseSeed(faker.Random.Int());

                                var matches = matchFaker.Generate(this.MatchCount(fakeState, challenge.BestOf));

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
            FakerHub.User().Reset();

            return base.Generate(ruleSets);
        }
    }
}
