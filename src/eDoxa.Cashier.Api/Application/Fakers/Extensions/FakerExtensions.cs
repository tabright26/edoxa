// Filename: FakerExtensions.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Cashier.Api.Application.Fakers.DataSets;
using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Application.Fakers.Extensions
{
    public static class FakerExtensions
    {
        private static ICollection<UserId> _testUserIds = CashierStorage.TestUserIds.OrderBy(testUserId => testUserId).ToList();

        public static UserId UserId(this Faker faker)
        {
            if (!_testUserIds.Any())
            {
                throw new ApplicationException("There is no longer any test user ID available.");
            }

            var testUserId = faker.PickRandom(_testUserIds);

            _testUserIds.Remove(testUserId);

            return Domain.AggregateModels.AccountAggregate.UserId.FromGuid(testUserId);
        }

        public static ChallengeDataSet Challenge(this Faker faker)
        {
            return new ChallengeDataSet(faker);
        }

        public static ChallengeSetupDataSet Setup(this Faker faker)
        {
            return new ChallengeSetupDataSet(faker);
        }
    }
}
