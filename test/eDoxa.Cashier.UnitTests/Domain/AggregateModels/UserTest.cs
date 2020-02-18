// Filename: PriceTest.cs
// Date Created: 2020-02-17
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
{
    public sealed class UserTest : UnitTest
    {
        public UserTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public void UserId_ShouldBeSameAsUserId()
        {
            // Arrange
            var userId = new UserId();
            var user = new User(userId);

            // Act
            var id = user.Id;

            // Assert
            id.Should().Be(userId);
        }
    }
}
