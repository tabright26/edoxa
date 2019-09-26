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

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage
{
    public sealed class ArenaChallengeTestFileStorage : IArenaChallengeTestFileStorage
    {
        private readonly IChallengeFaker _challengeFaker;
        private readonly CloudFileShare _share;

        public ArenaChallengeTestFileStorage(IChallengeFakerFactory challengeFakerFactory)
        {
            _challengeFaker = challengeFakerFactory.CreateInstance(null, null);

            var storageCredentials = new StorageCredentials(
                "edoxadev",
                "KjHiR9rgn7tLkyKl4fK8xsAH6+YAgTqX8EyHdy+mIEFaGQTtVdAnS2jmVkfzynLFnBzjJOSyHu6WR44eqWbUXA==");

            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, false);

            var cloudBlobClient = cloudStorageAccount.CreateCloudFileClient();

            _share = cloudBlobClient.GetShareReference("arena-challenge");
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
                    })
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
