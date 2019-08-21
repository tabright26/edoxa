// Filename: ArenaChallengeTestFileStorage.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public sealed class ArenaChallengeTestFileStorage : IArenaChallengeTestFileStorage
    {
        private readonly CloudFileShare _share;
        private readonly ScoringFactory _scoringFactory;

        public ArenaChallengeTestFileStorage()
        {
            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA=="
            );

            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = cloudStorageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("arena-challenge");

            _scoringFactory = new ScoringFactory();
        }

        public async Task<IImmutableSet<User>> GetUsersAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("Users.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(Guid)
                    }
                )
                .Select(record => new User(UserId.FromGuid(record.Id)))
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<IChallenge>> GetChallengesAsync()
        {
            if (!await _share.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage file share reference does not exist.");
            }

            var rootDirectory = _share.GetRootDirectoryReference();

            var test = rootDirectory.GetDirectoryReference("test");

            if (!await test.ExistsAsync())
            {
                throw new InvalidOperationException("The Azure Storage folder 'test' does not exist in the 'identity' share'.");
            }

            var file = test.GetFileReference("Challenges.csv");

            if (!await file.ExistsAsync())
            {
                throw new InvalidOperationException();
            }

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
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
                .Select(
                    record =>
                    {
                        var challengeFaker = new ChallengeFaker();

                        challengeFaker.CustomInstantiator(
                            faker =>
                            {
                                faker.User().Reset();

                                var name = new ChallengeName(record.Name!);

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

                                var scoringStrategy = _scoringFactory.CreateInstance(game);

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

                        return challengeFaker.Generate();
                    }
                )
                .ToImmutableHashSet();

            static int ParticipantCount(Faker faker, ChallengeState state, Entries entries)
            {
                return state == ChallengeState.Inscription ? new Entries(faker.Random.Int(1, entries - 1)) : entries;
            }

            static int MatchCount(Faker faker, ChallengeState state, BestOf bestOf)
            {
                return state != ChallengeState.Inscription ? faker.Random.Int(1, bestOf + 3) : 0;
            }
        }
    }
}
