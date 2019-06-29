// Filename: UserFakerExtensions.cs
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

using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Seedwork.Common.Extensions
{
    // TODO: To refactor.
    public static class UserFakerExtensions
    {
        private static ICollection<Guid> _testUserIds = DataResources.TestUserIds.OrderBy(testUserId => testUserId).ToList();

        public static UserId UserId(this Faker faker)
        {
            if (!_testUserIds.Any())
            {
                throw new ApplicationException("There is no longer any test user ID available.");
            }

            var testUserId = faker.PickRandom(_testUserIds);

            _testUserIds.Remove(testUserId);

            return ValueObjects.UserId.FromGuid(testUserId);
        }

        public static void ResetUserIds()
        {
            _testUserIds = DataResources.TestUserIds.OrderBy(testUserId => testUserId).ToList();
        }
    }
}
