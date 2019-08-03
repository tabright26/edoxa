// Filename: ArenaChallengesStorage.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Bogus;

using CsvHelper;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public static class ArenaChallengesStorage
    {
        private const string TestUsersFilePath = "Infrastructure/Data/Storage/TestFiles/TestUsers.csv";
        private const string TestChallengesFilePath = "Infrastructure/Data/Storage/TestFiles/TestChallenges.csv";

        public static IReadOnlyCollection<IChallenge> TestChallenges => GetTestChallenges().ToList();

        public static IReadOnlyCollection<User> TestUsers => GetTestUsers().ToList();

        public static User TestAdmin => TestUsers.First();

        private static IEnumerable<User> GetTestUsers()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestUsersFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                new
                {
                    Id = default(Guid)
                }
            );

            foreach (var record in records)
            {
                yield return new User(UserId.FromGuid(record.Id));
            }
        }

        private static IEnumerable<IChallenge> GetTestChallenges()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), TestChallengesFilePath);

            using var reader = new StreamReader(path);

            using var csv = new CsvReader(reader);

            var records = csv.GetRecords(
                    new
                    {
                        Id = default(Guid),
                        Name = default(string),
                        Game = default(int),
                        Entries = default(int),
                        BestOf = default(int),
                        Duration = default(long),
                        State = default(int)
                    }
                )
                .ToList();

            var scoringFactory = new ScoringFactory();

            foreach (var record in records)
            {
                var challengeFaker = new ChallengeFaker();

                challengeFaker.CustomInstantiator(
                    faker =>
                    {
                        faker.User().Reset();

                        var name = new ChallengeName(record.Name);

                        var game = ChallengeGame.FromValue(record.Game)!;

                        var entries = new Entries(record.Entries);

                        var bestOf = new BestOf(record.BestOf);

                        var duration = new ChallengeDuration(TimeSpan.FromTicks(record.Duration));

                        var state = ChallengeState.FromValue(record.State)!;

                        var utcNowDate = DateTime.UtcNow.Date;

                        var createdAt = faker.Date.Recent(1, utcNowDate);

                        var startedAt = faker.Date.Between(createdAt, utcNowDate);

                        var endedAt = startedAt + duration;

                        var closedAt = faker.Date.Soon(1, endedAt);

                        var synchronizedAt = faker.Date.Between(startedAt, closedAt);

                        var timeline = new ChallengeTimeline(new DateTimeProvider(startedAt), duration);

                        var scoringStrategy = scoringFactory.CreateInstance(game);

                        var challenge = new Challenge(
                            name,
                            game,
                            bestOf,
                            entries,
                            timeline,
                            scoringStrategy.Scoring
                        );

                        challenge.SetEntityId(ChallengeId.FromGuid(record.Id));

                        var participantFaker = new ParticipantFaker(game, createdAt, startedAt);

                        participantFaker.UseSeed(faker.Random.Int());

                        var participants = participantFaker.Generate(ParticipantCount(faker, state, challenge.Entries));

                        participants.ForEach(participant => challenge.Register(participant));

                        if (state != ChallengeState.Inscription)
                        {
                            challenge.Start(new DateTimeProvider(startedAt));

                            participants.ForEach(
                                participant =>
                                {
                                    var matchFaker = new MatchFaker(game, challenge.Scoring, synchronizedAt);

                                    matchFaker.UseSeed(faker.Random.Int());

                                    var matches = matchFaker.Generate(MatchCount(faker, state, challenge.BestOf));

                                    matches.ForEach(participant.Snapshot);
                                }
                            );

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
                    }
                );

                yield return challengeFaker.Generate();
            }
        }

        private static int ParticipantCount(Faker faker, ChallengeState state, Entries entries)
        {
            return state == ChallengeState.Inscription ? new Entries(faker.Random.Int(1, entries - 1)) : entries;
        }

        private static int MatchCount(Faker faker, ChallengeState state, BestOf bestOf)
        {
            return state != ChallengeState.Inscription ? faker.Random.Int(1, bestOf + 3) : 0;
        }
    }
}
