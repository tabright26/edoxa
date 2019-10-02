// Filename: ChallengeTest.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
{
    [TestClass]
    public sealed class MemberTest
    {
        [TestMethod]
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

        [TestMethod]
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
