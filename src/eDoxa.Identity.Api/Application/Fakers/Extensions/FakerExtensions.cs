// Filename: FakerExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;
using eDoxa.Identity.Domain.AggregateModels;

namespace eDoxa.Identity.Api.Application.Fakers.Extensions
{
    public static class FakerExtensions
    {
        private static ICollection<UserId> _testUsers = IdentityStorage.TestUserIds.OrderBy(testUserId => testUserId).ToList();

        public static UserId UserId(this Faker faker)
        {
            if (!_testUsers.Any())
            {
                throw new ApplicationException("There is no longer any test user ID available.");
            }

            var testUserId = faker.PickRandom(_testUsers);

            _testUsers.Remove(testUserId);

            return testUserId;
        }

        public static void ResetUserIds()
        {
            _testUsers = IdentityStorage.TestUserIds.OrderBy(testUserId => testUserId).ToList();
        }
    }
}
