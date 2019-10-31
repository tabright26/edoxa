// Filename: InvitationTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.TestHelper;
using eDoxa.Organizations.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
{
    public sealed class InvitationTest : UnitTest
    {
        public InvitationTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void Contructor_Tests()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            // Act
            var invitation = new Invitation(userId, clanId);

            // Assert
            invitation.Id.Should().BeOfType(typeof(InvitationId));

            invitation.UserId.Should().Be(userId);
            invitation.UserId.Should().NotBeNull();

            invitation.ClanId.Should().Be(clanId);
            invitation.ClanId.Should().NotBeNull();
        }
    }
}
