// Filename: ChallengeFaker.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    internal sealed partial class ChallengeFaker : IChallengeFaker
    {
        private readonly ScoringFactory _scoringFactory = new ScoringFactory();

        public IChallenge FakeChallenge(string? ruleSets = null)
        {
            return this.Generate(ruleSets);
        }

        public IChallenge FakeChallenge(ChallengeModel model)
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.CustomInstantiator(
                faker =>
                {
                    faker.User().Reset();

                    var name = new ChallengeName(model.Name);

                    var game = ChallengeGame.FromValue(model.Game);

                    var entries = new Entries(model.Entries);

                    var bestOf = new BestOf(model.BestOf);

                    var duration = new ChallengeDuration(TimeSpan.FromTicks(model.Timeline.Duration));

                    var state = ChallengeState.FromValue(model.State);

                    var utcNowDate = DateTime.UtcNow.Date;

                    var createdAt = faker.Date.Recent(1, utcNowDate);

                    var startedAt = faker.Date.Between(createdAt, utcNowDate);

                    var endedAt = startedAt + duration;

                    var closedAt = faker.Date.Soon(1, endedAt);

                    var synchronizedAt = faker.Date.Between(startedAt, closedAt);

                    var timeline = new ChallengeTimeline(new DateTimeProvider(startedAt), duration);

                    var scoringStrategy = _scoringFactory.CreateInstance(game);

                    var challenge = new Challenge(
                        name,
                        game,
                        bestOf,
                        entries,
                        timeline,
                        scoringStrategy.Scoring);

                    challenge.SetEntityId(ChallengeId.FromGuid(model.Id));

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
                                var matchFaker = new MatchFaker(game, challenge.Scoring, synchronizedAt);

                                matchFaker.UseSeed(faker.Random.Int());

                                var matches = matchFaker.Generate(this.MatchCount(state, challenge.BestOf));

                                matches.ForEach(participant.Snapshot);
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
        public ChallengeFaker(ChallengeGame? game = null, ChallengeState? state = null)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var fakeGame = faker.Challenge().Game(game);

                    var fakeState = faker.Challenge().State(state);

                    var scoring = _scoringFactory.CreateInstance(fakeGame).Scoring;

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
                        scoring);

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
            public ParticipantFaker(ChallengeGame game, DateTime createdAt, DateTime startedAt)
            {
                this.CustomInstantiator(
                    faker =>
                    {
                        var participant = new Participant(
                            faker.User().Id(),
                            faker.Participant().GameAccountId(game),
                            new DateTimeProvider(FakerHub.Date.Between(createdAt, startedAt)));

                        participant.SetEntityId(faker.Participant().Id());

                        return participant;
                    });
            }
        }

        private class MatchFaker : Faker<IMatch>
        {
            public MatchFaker(ChallengeGame game, IScoring scoring, DateTime synchronizedAt)
            {
                this.CustomInstantiator(
                    faker =>
                    {
                        var match = new StatMatch(
                            scoring,
                            faker.Game().Stats(game),
                            faker.Game().Reference(game),
                            new DateTimeProvider(synchronizedAt));

                        match.SetEntityId(faker.Match().Id());

                        return match;
                    });
            }
        }
    }
}
