// Filename: ChallengeTest.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Organizations.Clans.UnitTests.Domain.Models
{
    [TestClass]
    public sealed class ClanTest
    {
        [TestMethod]
        public void Contructor_Tests()
        {
            // Arrange
            var ownerId = new UserId();
            var name = "TestClan";

            // Act
            var clan = new Clan(name, ownerId);

            // Assert
            clan.Id.Should().BeOfType(typeof(ClanId));

            clan.OwnerId.Should().BeOfType(typeof(UserId));
            clan.OwnerId.Should().Be(ownerId);
            clan.OwnerId.Should().NotBeNull();

            clan.Name.Should().Be(name);
            clan.Name.Should().NotBeNull();

            clan.Members.Should().BeOfType(typeof(HashSet<Member>));
            clan.Members.Should().HaveCount(1);
            clan.Members.Should().NotBeNull();

            clan.Summary.Should().BeNull();
        }

        [DataRow(15,16)]
        [DataRow(1,2)]
        [DataTestMethod]
        public void AddMemberFromCandidature_WithAmount_ShouldHaveCount(int amount, int result)
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act
            for (var i = 0; i < amount; i++)
            {
                clan.AddMember(new Candidature(new UserId(), clan.Id));
            }


            // Assert
            clan.Members.Should().NotBeNull();
            clan.Members.Should().HaveCount(result);
        }

        [DataRow(15,16)]
        [DataRow(1,2)]
        [DataRow(0,1)]
        [DataTestMethod]
        public void AddMemberFromInvitation_WithAmount_ShouldHaveCount(int amount, int result)
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act
            for (var i = 0; i < amount; i++)
            {
                clan.AddMember(new Invitation(new UserId(), clan.Id));
            }


            // Assert
            clan.Members.Should().NotBeNull();
            clan.Members.Should().HaveCount(result);
        }

        [DataRow(15)]
        [DataRow(1)]
        [DataRow(0)]
        [DataTestMethod]
        public void LeaveAsUser_WithInitialAdditionalMemberAmount_ShouldHaveMemberAmount(int amount)
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            for (var i = 0; i < amount; i++)
            {
                clan.AddMember(new Invitation(new UserId(), clan.Id));
            }

            // Act
            clan.Leave(clan.Members.ElementAt(amount));

            // Assert
            clan.Members.Should().BeOfType(typeof(HashSet<Member>));
            clan.Members.Should().HaveCount(amount);
            clan.Members.Should().NotBeNull();
        }

        [DataRow(15)]
        [DataRow(1)]
        [DataRow(0)]
        [DataTestMethod]
        public void LeaveAsOwner_WithInitialAdditionalMemberAmount_ShouldHaveMemberAmount(int amount)
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            for (var i = 0; i < amount; i++)
            {
                clan.AddMember(new Invitation(new UserId(), clan.Id));
            }

            // Act
            clan.Leave(clan.Members.ElementAt(0));

            // Assert
            clan.Members.Should().BeOfType(typeof(HashSet<Member>));
            clan.Members.Should().HaveCount(amount);
            clan.Members.Should().NotBeNull();
        }

        [DataRow(15)]
        [DataRow(1)]
        [DataTestMethod]
        public void KickAsOwner_WithInitialAdditionalMemberAmount_ShouldHaveMemberAmount(int amount)
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            for (var i = 0; i < amount; i++)
            {
                clan.AddMember(new Invitation(new UserId(), clan.Id));
            }

            // Act
            clan.Kick(clan.Members.ElementAt(amount));

            // Assert
            clan.Members.Should().BeOfType(typeof(HashSet<Member>));
            clan.Members.Should().HaveCount(amount);
            clan.Members.Should().NotBeNull();
        }

        [TestMethod]
        public void MemberIsOwner_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("TestClan", userId);

            // Act Assert
            clan.MemberIsOwner(userId).Should().BeTrue();
        }

        [TestMethod]
        public void MemberIsOwner_ShouldBeFalse()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act Assert
            clan.MemberIsOwner(new UserId()).Should().BeFalse();
        }

        [TestMethod]
        public void CanDelegateOwnership_ShouldBeTrue()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());
            clan.AddMember(new Invitation(new UserId(), clan.Id));

            // Act Assert
            clan.CanDelegateOwnership().Should().BeTrue();
        }

        [TestMethod]
        public void CanDelegateOwnership_ShouldBeFalse()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act Assert
            clan.CanDelegateOwnership().Should().BeFalse();
        }

        [TestMethod]
        public void HasMember_WithUserId_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("TestClan", userId);

            // Act Assert
            clan.HasMember(userId).Should().BeTrue();
        }

        [TestMethod]
        public void HasMember_WithUserId_ShouldBeFalse()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act Assert
            clan.HasMember(new UserId()).Should().BeFalse();
        }

        [TestMethod]
        public void HasMember_WithMemberId_ShouldBeTrue()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            var memberId = clan.Members.SingleOrDefault()?.Id;

            // Act Assert
            clan.HasMember(memberId).Should().BeTrue();
        }

        [TestMethod]
        public void HasMember_WithMemberId_ShouldBeFalse()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            // Act Assert
            clan.HasMember(new MemberId()).Should().BeFalse();
        }

        [TestMethod]
        public void FindMember_WithUserId_ShouldNotBeNull()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("TestClan", userId);

            // Act
            var member = clan.FindMember(userId);

            // Assert
            member.Should().NotBeNull();
        }

        [TestMethod]
        public void FindMember_WithMemberId_ShouldNotBeNull()
        {
            // Arrange
            var clan = new Clan("TestClan", new UserId());

            var memberId = clan.Members.SingleOrDefault()?.Id;

            // Act
            var member = clan.FindMember(memberId);

            // Assert
            member.Should().NotBeNull();

        }

        //TODO: SHOULD I  DO THE IsDelete() function from clan ??? Cause it calls a domain event.
    }
}
