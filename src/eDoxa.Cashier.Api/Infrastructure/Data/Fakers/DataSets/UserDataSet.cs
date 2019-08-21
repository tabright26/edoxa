// Filename: UserDataSet.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using Bogus;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Fakers.DataSets
{
    public class UserDataSet
    {
        private static readonly IImmutableSet<User> TestUsers = new CashierTestFileStorage().GetUsersAsync().Result;

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
