// Filename: CredentialTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Games.UnitTests.Domain.AggregateModels.CredentialAggregate
{
    public sealed class CredentialTest : UnitTest
    {
        public CredentialTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void Contructor_Tests()
        {
            // Arrange
            var userId = new UserId();
            var playerId = new PlayerId();

            var timestamp = new UtcNowDateTimeProvider();

            // Act
            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                playerId,
                timestamp);

            // Assert
            credential.UserId.Should().Be(userId);
            credential.UserId.Should().NotBeNull();

            credential.Game.Should().Be(Game.LeagueOfLegends);
            credential.Game.Should().NotBeNull();

            credential.PlayerId.Should().Be(playerId);
            credential.PlayerId.Should().NotBeNull();

            credential.Timestamp.Should().Be(timestamp.DateTime);
        }
    }
}
