// Filename: MemberTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
{
    public sealed class MemberTest : UnitTest
    {
        public MemberTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void Contructor_WithCandidature_Tests()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            // Act
            var member = new Member(new Candidature(userId, clanId));

            // Assert
            member.Id.Should().BeOfType(typeof(MemberId));

            member.UserId.Should().Be(userId);
            member.UserId.Should().NotBeNull();

            member.ClanId.Should().Be(clanId);
            member.ClanId.Should().NotBeNull();
        }

        [Fact]
        public void Contructor_WithInvitation_Tests()
        {
            // Arrange
            var userId = new UserId();
            var clanId = new ClanId();

            // Act
            var member = new Member(new Invitation(userId, clanId));

            // Assert
            member.Id.Should().BeOfType(typeof(MemberId));

            member.UserId.Should().Be(userId);
            member.UserId.Should().NotBeNull();

            member.ClanId.Should().Be(clanId);
            member.ClanId.Should().NotBeNull();
        }
    }
}
