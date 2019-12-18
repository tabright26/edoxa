// Filename: UserDataSet.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers.DataSets
{
    public class UserDataSet
    {
        private static ICollection<UserId> _testUsers = new HashSet<UserId>(FileStorage.Users);

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

            return testUser;
        }

        public void Reset()
        {
            _testUsers = new HashSet<UserId>(FileStorage.Users);
        }
    }
}
