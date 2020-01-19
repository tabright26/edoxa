// Filename: ChallengeFaker.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers
{
    internal sealed partial class ChallengeFaker : IChallengeFaker
    {
        public IChallenge FakeChallenge(string? ruleSets = null)
        {
            return this.Generate(ruleSets);
        }

        public IReadOnlyCollection<IChallenge> FakeChallenges(int count, string? ruleSets = null)
        {
            return this.Generate(count, ruleSets);
        }

        public IChallenge FakeChallenge(ChallengeModel model)
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.CustomInstantiator(
                faker =>
                {
                    faker.User().Reset();

                    var name = new ChallengeName(model.Name);

                    var game = Game.FromValue(model.Game);

                    var entries = new Entries(model.Entries);

                    var bestOf = new BestOf(model.BestOf);

                    var duration = new ChallengeDuration(TimeSpan.FromTicks(model.Timeline.Duration));

                    var state = ChallengeState.FromValue(model.State);

                    var utcNowDate = DateTime.UtcNow.Date;

                    var createdAt = faker.Date.Recent(1, utcNowDate);

                    var startedAt = faker.Date.Between(createdAt, utcNowDate);

                    var endedAt = startedAt + duration;

                    var synchronizationBuffer = endedAt + TimeSpan.FromHours(2);

                    var closedAt = faker.Date.Soon(1, synchronizationBuffer);

                    var synchronizedAt = faker.Date.Between(synchronizationBuffer, closedAt);

                    var timeline = new ChallengeTimeline(new DateTimeProvider(startedAt), duration);

                    var scoring = new Scoring
                    {
                        [new StatName("StatName1")] = new StatWeighting(0.00015F),
                        [new StatName("StatName2")] = new StatWeighting(1),
                        [new StatName("StatName3")] = new StatWeighting(0.77F),
                        [new StatName("StatName4")] = new StatWeighting(100),
                        [new StatName("StatName5")] = new StatWeighting(-3)
                    };

                    var challenge = new Challenge(
                        model.Id.ConvertTo<ChallengeId>(),
                        name,
                        game,
                        bestOf,
                        entries,
                        timeline,
                        scoring);

                    var participantFaker = new ParticipantFaker(game, createdAt, startedAt);

                    participantFaker.UseSeed(faker.Random.Int());

                    var participants = participantFaker.Generate(this.ParticipantCount(state, challenge.Entries));

                    participants.ForEach(participant => challenge.Register(participant));

                    if (state != ChallengeState.Inscription)
                    {
                        challenge.Start(new DateTimeProvider(startedAt));

                        participants.ForEach(
                            participant =>
                            {
                                var matchFaker = new MatchFaker(challenge.Scoring);

                                matchFaker.UseSeed(faker.Random.Int());

                                var matches = matchFaker.Generate(this.MatchCount(state, challenge.BestOf));

                                participant.Snapshot(matches, new DateTimeProvider(synchronizedAt));
                            });

                        challenge.Synchronize(new DateTimeProvider(synchronizedAt));

                        if (state == ChallengeState.Ended || state == ChallengeState.Closed)
                        {
                            challenge.Start(new DateTimeProvider(startedAt - duration));
                        }

                        if (state == ChallengeState.Closed)
                        {
                            challenge.Close(new DateTimeProvider(closedAt));
                        }
                    }

                    return challenge;
                });

            return challengeFaker.Generate();
        }
    }

    internal sealed partial class ChallengeFaker : Faker<IChallenge>
    {
        public ChallengeFaker(Game? game = null, ChallengeState? state = null)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var fakeGame = faker.Challenge().Game(game);

                    var fakeState = faker.Challenge().State(state);

                    var scoring = new Scoring
                    {
                        [new StatName("StatName1")] = new StatWeighting(0.00015F),
                        [new StatName("StatName2")] = new StatWeighting(1),
                        [new StatName("StatName3")] = new StatWeighting(0.77F),
                        [new StatName("StatName4")] = new StatWeighting(100),
                        [new StatName("StatName5")] = new StatWeighting(-3)
                    };

                    var duration = faker.Challenge().Duration();

                    var utcNowDate = DateTime.UtcNow.Date;

                    var createdAt = faker.Date.Recent(1, utcNowDate);

                    var startedAt = faker.Date.Between(createdAt, utcNowDate);

                    var endedAt = startedAt + duration;

                    var synchronizationBuffer = endedAt + TimeSpan.FromHours(2);

                    var closedAt = faker.Date.Soon(1, synchronizationBuffer);

                    var synchronizedAt = faker.Date.Between(synchronizationBuffer, closedAt);

                    var challenge = new Challenge(
                        faker.Challenge().Id(),
                        faker.Challenge().Name(),
                        fakeGame,
                        faker.Challenge().BestOf(),
                        faker.Challenge().Entries(),
                        new ChallengeTimeline(new DateTimeProvider(createdAt), duration),
                        scoring);

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
                                var matchFaker = new MatchFaker(scoring);

                                matchFaker.UseSeed(faker.Random.Int());

                                var matches = matchFaker.Generate(this.MatchCount(fakeState, challenge.BestOf));

                                participant.Snapshot(matches, new DateTimeProvider(synchronizedAt));
                            });

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
                });
        }

        private int ParticipantCount(ChallengeState state, Entries entries)
        {
            return state == ChallengeState.Inscription ? new Entries(FakerHub.Random.Int(1, entries - 1)) : entries;
        }

        private int MatchCount(ChallengeState state, BestOf bestOf)
        {
            return state != ChallengeState.Inscription ? FakerHub.Random.Int(1, bestOf + 3) : 0;
        }

        public override IChallenge Generate(string? ruleSets = null)
        {
            FakerHub.User().Reset();

            return base.Generate(ruleSets);
        }

        private sealed class ParticipantFaker : Faker<Participant>
        {
            public ParticipantFaker(Game game, DateTime createdAt, DateTime startedAt)
            {
                this.CustomInstantiator(
                    faker => new Participant(
                        faker.Participant().Id(),
                        faker.User().Id(),
                        faker.Participant().PlayerId(game),
                        new DateTimeProvider(FakerHub.Date.Between(createdAt, startedAt))));
            }
        }

        private class MatchFaker : Faker<IMatch>
        {
            public MatchFaker(IScoring scoring)
            {
                this.CustomInstantiator(
                    faker =>
                    {
                        var match = new Match(
                            faker.Game().Uuid(),
                            new UtcNowDateTimeProvider(),
                            TimeSpan.FromSeconds(3600),
                            scoring.Map(faker.Game().Stats()),
                            new UtcNowDateTimeProvider());

                        match.SetEntityId(faker.Match().Id());

                        return match;
                    });
            }
        }
    }
}
