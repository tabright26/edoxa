// Filename: CandidatureTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright � 2020, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Clans.UnitTests.Domain.AggregateModels.CandidatureAggregate
{
    public sealed class CandidatureTest : UnitTest
    {
        public CandidatureTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void Contructor_Tests()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            // Act
            var candidature = new Candidature(userId, clanId);

            // Assert
            candidature.Id.Should().BeOfType(typeof(CandidatureId));

            candidature.UserId.Should().Be(userId);
            candidature.UserId.Should().NotBeNull();

            candidature.ClanId.Should().Be(clanId);
            candidature.ClanId.Should().NotBeNull();
        }
    }
}
