// Filename: CandidatureTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
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
