// Filename: UserDataSet.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Identity.Api.Infrastructure.Data.Fakers.DataSets
{
    public sealed class UserDataSet
    {
        private static readonly IReadOnlyCollection<User> TestUsers = IdentityStorage.TestUsers;

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

            return UserId.FromGuid(testUser.Id);
        }

        public void Reset()
        {
            _testUsers = new HashSet<User>(TestUsers);
        }
    }
}
