// Filename: SynchronizeChallengeTest.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Worker.Application.RecurringJobs;
using eDoxa.Challenges.Worker.Extensions;
using eDoxa.FunctionalTests.TestHelper.Services.Challenges;
using eDoxa.FunctionalTests.TestHelper.Services.Games;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.TestHelper.Extensions;

using Xunit;

namespace eDoxa.FunctionalTests
{
    public sealed class SynchronizeChallengeTest
    {
        [Fact(Skip = "")]
        public async Task Test1()
        {
            // Arrange
            using var challengesHost = new ChallengesHostFactory();
            using var gamesHost = new GamesHostFactory();

            var recurringJob = new ChallengeRecurringJob(
                challengesHost.CreateChannel().CreateChallengeServiceClient(),
                gamesHost.CreateChannel().CreateGameServiceClient());

            // Act
            await recurringJob.SynchronizeChallengeAsync(GameDto.LeagueOfLegends);
        }
    }
}
