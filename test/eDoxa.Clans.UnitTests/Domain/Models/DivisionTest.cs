// Filename: DivisionTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Clans.UnitTests.Domain.Models
{
    public sealed class DivisionTest : UnitTest
    {
        public DivisionTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [InlineData(15)]
        [InlineData(1)]
        [Theory]
        public void AddMember_WithAmount_ShouldHaveCount(int amount)
        {
            // Arrange
            var clanId = new ClanId();

            var division = new Division(clanId, "test", "division");

            // Act
            for (var i = 0; i < amount; i++)
            {
                division.AddMember(new Member(clanId, new UserId()));
            }

            // Assert
            division.Members.Should().NotBeNull();
            division.Members.Should().HaveCount(amount);
        }

        [InlineData(15)]
        [InlineData(1)]
        [Theory]
        public void RemoveMember_WithAmount_ShouldHaveCount(int amount)
        {
            // Arrange
            var clanId = new ClanId();

            var division = new Division(clanId, "test", "division");

            // Act
            for (var i = 0; i < amount; i++)
            {
                division.AddMember(new Member(clanId, new UserId()));
            }

            for (var i = 0; i < amount; i++)
            {
                division.RemoveMember(division.Members.FirstOrDefault());
            }

            // Assert
            division.Members.Should().NotBeNull();
            division.Members.Should().HaveCount(0);
        }

        [InlineData("test", "division")]
        [InlineData("test", "")]
        [InlineData("", "test")]
        [Theory]
        public void Update_WithParameters_ShouldChange(string name, string description)
        {
            // Arrange
            var clanId = new ClanId();

            var division = new Division(clanId, "test", "division");

            // Act
            division.Update(name, description);

            // Assert
            division.Name.Should().Be(name);
            division.Name.Should().NotBeNull();

            division.Description.Should().Be(description);
            division.Description.Should().NotBeNull();
        }

        [Fact]
        public void Contructor_Tests()
        {
            // Arrange
            var ownerId = new UserId();
            var clanId = new ClanId();

            var name = "test";
            var description = "division";

            // Act
            var division = new Division(clanId, name, description);

            // Assert
            division.Id.Should().BeOfType(typeof(DivisionId));

            division.Name.Should().Be(name);
            division.Name.Should().NotBeNull();

            division.Description.Should().Be(description);
            division.Description.Should().NotBeNull();

            division.Members.Should().BeOfType(typeof(HashSet<Member>));
            division.Members.Should().HaveCount(0);
            division.Members.Should().NotBeNull();
        }

        [Fact]
        public void HasMember_WithMemberId_ShouldBeFalse()
        {
            // Arrange
            var division = new Division(new ClanId(), "test", "division");

            // Act Assert
            division.HasMember(new MemberId()).Should().BeFalse();
        }

        [Fact]
        public void HasMember_WithMemberId_ShouldBeTrue()
        {
            // Arrange
            var clanId = new ClanId();

            var division = new Division(clanId, "test", "division");

            division.AddMember(new Member(clanId, new UserId()));

            var memberId = division.Members.SingleOrDefault()?.Id;

            // Act Assert
            division.HasMember(memberId).Should().BeTrue();
        }
    }
}
