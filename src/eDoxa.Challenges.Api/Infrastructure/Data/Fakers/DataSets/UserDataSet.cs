// Filename: UserDataSet.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    // TODO: Should be refactored.
    public sealed class UserDataSet
    {
        private static ICollection<User> _testUsers = new HashSet<User>(FileStorage.Users);

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
            _testUsers = new HashSet<User>(FileStorage.Users);
        }
    }
}
