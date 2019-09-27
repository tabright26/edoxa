// Filename: UserDataSet.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Storage.Azure.File;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    // TODO: Should be refactored.
    public sealed class UserDataSet
    {
        private static readonly IImmutableSet<User> TestUsers =
            new ArenaChallengeTestFileStorage(new AzureFileStorage(), new ChallengeFakerFactory()).GetUsersAsync().Result;

        private static ICollection<User> _testUsers = new HashSet<User>(TestUsers);

        public UserDataSet(Faker faker)
        {
            Faker = faker;
        }

        private Faker Faker { get; }

        public UserId Id()
        {
            if (!_testUsers.Any())
            {
                this.Reset();
            }

            var testUser = Faker.PickRandom(_testUsers);

            _testUsers.Remove(testUser);

            return testUser.Id;
        }

        public void Reset()
        {
            _testUsers = new HashSet<User>(TestUsers);
        }
    }
}
