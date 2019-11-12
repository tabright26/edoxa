// Filename: AuthFactorTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Games.UnitTests.Domain.AggregateModels.AuthFactorAggregate
{
    public sealed class AuthFactorTest : UnitTest
    {
        public AuthFactorTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void Contructor_Tests()
        {
            // Arrange
            var key = "playerId";
            var playerId = new PlayerId();

            // Act
            var authFactor = new AuthFactor(playerId, key);

            // Assert
            authFactor.PlayerId.Should().Be(playerId);
            authFactor.PlayerId.Should().NotBeNull();

            authFactor.Key.Should().Be(key);
            authFactor.Key.Should().NotBeNull();
        }

    }
}
