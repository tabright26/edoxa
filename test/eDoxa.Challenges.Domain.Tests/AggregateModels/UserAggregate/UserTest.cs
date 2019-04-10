// Filename: UserTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class UserTest
    {
        private static readonly UserAggregateFactory _factory = UserAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_User_ShouldNotBeNull()
        {
            // Act
            var user = _factory.CreateUser();

            // Assert
            user.Should().NotBeNull();
        }
    }
}