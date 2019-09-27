// Filename: ArenaChallengeTestFileStorage.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Storage.Azure.File.Abstractions;
using eDoxa.Storage.Azure.File.Extensions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public sealed class ArenaChallengeTestFileStorage : IArenaChallengeTestFileStorage
    {
        private readonly IAzureFileStorage _fileStorage;
        private readonly IChallengeFaker _challengeFaker;

        public ArenaChallengeTestFileStorage(IAzureFileStorage fileStorage, IChallengeFakerFactory factory)
        {
            _fileStorage = fileStorage;
            _challengeFaker = factory.CreateFaker(null);
        }

        public async Task<IImmutableSet<User>> GetUsersAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("Users.csv");

            using var csvReader = await file.OpenCsvReaderAsync();

            return csvReader.GetRecords(
                    new
                    {
                        Id = default(Guid)
                    })
                .Select(record => new User(UserId.FromGuid(record.Id)))
                .ToImmutableHashSet();
        }

        public async Task<IImmutableSet<IChallenge>> GetChallengesAsync()
        {
            var root = await _fileStorage.GetRootDirectory();

            var directory = await root.GetDirectoryAsync("test");

            var file = await directory.GetFileAsync("Challenges.csv");

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
                    })
                .Select(
                    record => _challengeFaker.FakeChallenge(
                        new ChallengeModel
                        {
                            Name = record.Name,
                            Game = record.Game,
                            Entries = record.Entries,
                            BestOf = record.BestOf,
                            Timeline = new ChallengeTimelineModel
                            {
                                Duration = record.Duration
                            },
                            State = record.State,
                            Id = record.Id
                        }))
                .ToImmutableHashSet();
        }
    }
}
