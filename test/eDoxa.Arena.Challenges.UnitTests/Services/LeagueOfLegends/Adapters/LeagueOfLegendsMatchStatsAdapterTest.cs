﻿// Filename: LeagueOfLegendsMatchStatsAdapterTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Services.LeagueOfLegends.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsMatchStatsAdapterTest
    {
        //[TestMethod]
        //public void SnapshotParticipantMatch_ShouldBeValid()
        //{
        //    // Arrange
        //    var matches = StubConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchDTO>>(@"Utilities/Stubs/LeagueOfLegends/Matches.json");

        //    var userId = new UserId();

        //    var externalAccount = new ExternalAccount("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");

        //    var challenge = FakeChallengeFactory.CreateChallenge();

        //    challenge.RegisterParticipant(userId, externalAccount);

        //    foreach (var match in matches)
        //    {
        //        var adapter = new LeagueOfLegendsMatchStatsAdapter(externalAccount, match);

        //         challenge.SnapshotParticipantMatch(challenge.Participants.Single(x => x.UserId == userId), new MatchReference(123123123123),  adapter.MatchStats);
        //    }

        //    // Act => Assert
        //    challenge.Participants.Single(x => x.UserId == userId).Matches.Should().HaveCount(5);
        //}
    }
}
