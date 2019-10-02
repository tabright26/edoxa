// Filename: ChallengeTest.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
{
    [TestClass]
    public sealed class InvitationTest
    {
        [TestMethod]
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
