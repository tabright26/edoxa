// Filename: LeagueOfLegendsMatchStatsAdapterTest.cs
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
        //private static readonly Faker Faker = new Faker();

        //[TestMethod]
        //public void SnapshotParticipantMatch_ShouldHaveCountOfFive()
        //{
        //    // Arrange
        //    var matches = StubConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchDto>>(@"Utilities/Stubs/LeagueOfLegends/Matches.json");

        //    var gameAccountId = new GameAccountId("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");

        //    var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);

        //    var challenge = challengeFaker.Generate();

        //    var participant = new Participant(Faker.UserId(), gameAccountId, new UtcNowDateTimeProvider());

        //    challenge.Register(participant);

        //    foreach (var match in matches)
        //    {
        //        var adapter = new LeagueOfLegendsMatchStatsAdapter(gameAccountId, match);

        //        challenge.SnapshotParticipantMatch(participant, new GameMatchId(match.GameId), adapter.MatchStats, new UtcNowDateTimeProvider());
        //    }

        //    // Act => Assert
        //    participant.Matches.Should().HaveCount(5);
        //}
    }
}
