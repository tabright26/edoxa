// Filename: SynchronizeChallengeTest.cs
// Date Created: 2019-12-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Worker.Application.RecurringJobs;
using eDoxa.Challenges.Worker.Extensions;
using eDoxa.FunctionalTests.TestHelper.Services.Challenges;
using eDoxa.FunctionalTests.TestHelper.Services.Games;
using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

using Scoring = eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Scoring;

namespace eDoxa.FunctionalTests
{
    public sealed class SynchronizeChallengeTest
    {
        private static readonly IScoring Scoring = new Scoring(
            new Dictionary<string, float>
            {
                ["StatName1"] = 5,
                ["StatName2"] = 10,
                ["StatName3"] = 15,
                ["StatName4"] = 20,
                ["StatName5"] = 25
            });

        public static IEnumerable<IChallenge> CreateChallenges()
        {
            var dateTimeProvider = new UtcNowDateTimeProvider();

            var challenge1 = new Challenge(
                new ChallengeId(),
                new ChallengeName("Challenge1"),
                Game.LeagueOfLegends,
                BestOf.One,
                Entries.Two,
                new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.OneDay),
                Scoring);

            challenge1.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    dateTimeProvider));

            challenge1.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    dateTimeProvider));

            challenge1.Start(dateTimeProvider);

            yield return challenge1;

            var challenge2 = new Challenge(
                new ChallengeId(),
                new ChallengeName("Challenge2"),
                Game.LeagueOfLegends,
                BestOf.One,
                Entries.Two,
                new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.OneDay),
                Scoring);

            challenge2.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    new UtcNowDateTimeProvider()));

            challenge2.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    new UtcNowDateTimeProvider()));

            challenge2.Start(dateTimeProvider);

            yield return challenge2;
        }

        public static IEnumerable<ChallengeMatch> CreateChallengeMatches()
        {
            yield return new ChallengeMatch(
                Guid.NewGuid().ToString(),
                new UtcNowDateTimeProvider(),
                new Dictionary<string, double>
                {
                    ["StatName1"] = 50,
                    ["StatName2"] = 100,
                    ["StatName3"] = 150,
                    ["StatName4"] = 200,
                    ["StatName5"] = 250
                });

            yield return new ChallengeMatch(
                Guid.NewGuid().ToString(),
                new UtcNowDateTimeProvider(),
                new Dictionary<string, double>
                {
                    ["StatName1"] = 50,
                    ["StatName2"] = 100,
                    ["StatName3"] = 150,
                    ["StatName4"] = 200,
                    ["StatName5"] = 250
                });
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(
                    challengeService => challengeService.GetMatchesAsync(
                        Game.LeagueOfLegends,
                        It.IsAny<PlayerId>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<DateTime?>()))
                .ReturnsAsync(CreateChallengeMatches().ToList())
                .Verifiable();

            //var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            //mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()));

            using var challengesHost = new ChallengesHostFactory();

            challengesHost.Server.CleanupDbContext();

            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IChallengeRepository>();

                    repository.Create(CreateChallenges().ToList());

                    await repository.CommitAsync(false);
                });

            using var gamesHost = new GamesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            //container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>().SingleInstance();

                            container.RegisterInstance(mockChallengeService.Object).As<IChallengeService>().SingleInstance();
                        });
                });

            var recurringJob = new ChallengeRecurringJob(
                challengesHost.CreateChannel().CreateChallengeServiceClient(),
                gamesHost.CreateChannel().CreateGameServiceClient());

            // Act
            await recurringJob.SynchronizeChallengeAsync(GameDto.LeagueOfLegends);

            // Assert
            mockChallengeService.Verify(
                challengeService => challengeService.GetMatchesAsync(
                    Game.LeagueOfLegends,
                    It.IsAny<PlayerId>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>()),
                Times.AtLeastOnce);
        }
    }
}
