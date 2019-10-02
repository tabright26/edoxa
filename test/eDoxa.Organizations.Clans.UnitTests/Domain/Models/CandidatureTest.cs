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
    public sealed class CandidatureTest
    {
        [TestMethod]
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
