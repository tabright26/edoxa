// Filename: InvitationTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Clans.UnitTests.Domain.AggregateModels.InvitationAggregate
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
