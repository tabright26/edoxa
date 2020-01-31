// Filename: SynchronizeChallengeTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Worker.Application.RecurringJobs;
using eDoxa.FunctionalTests.TestHelper.Services.Challenges;
using eDoxa.FunctionalTests.TestHelper.Services.Games;
using eDoxa.Games.Api.Grpc.Services;
using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Games.Domain.Services;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

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
            var dateTime = DateTime.UtcNow - ChallengeDuration.OneDay - TimeSpan.FromHours(2); // TODO: Quick fix.

            var dateTimeProvider = new DateTimeProvider(dateTime);

            var challenge1 = new Challenge(
                new ChallengeId(),
                new ChallengeName("Challenge1"),
                Game.LeagueOfLegends,
                BestOf.One,
                Entries.Two,
                new ChallengeTimeline(dateTimeProvider, ChallengeDuration.OneDay),
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
                new ChallengeTimeline(dateTimeProvider, ChallengeDuration.OneDay),
                Scoring);

            challenge2.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    dateTimeProvider));

            challenge2.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    dateTimeProvider));

            challenge2.Start(dateTimeProvider);

            yield return challenge2;
        }

        public static IEnumerable<ChallengeMatch> CreateChallengeMatches()
        {
            yield return new ChallengeMatch(
                Guid.NewGuid().ToString(),
                new UtcNowDateTimeProvider(),
                TimeSpan.FromHours(1),
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
                TimeSpan.FromHours(1),
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
        public async Task Fail()
        {
            // Arrange
            var challenges = CreateChallenges().ToList();

            var mockLogger = new MockLogger<GameGrpcService>();

            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(
                    challengeService => challengeService.GetMatchesAsync(
                        Game.LeagueOfLegends,
                        It.IsAny<PlayerId>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<IImmutableSet<string>>()))
                .Throws<Exception>()
                .Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            using var challengesHost = new ChallengesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>().SingleInstance();
                        });
                });

            challengesHost.Server.CleanupDbContext();

            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IChallengeRepository>();

                    repository.Create(challenges);

                    await repository.CommitAsync(false);
                });

            using var gamesHost = new GamesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockLogger.Object).SingleInstance();

                            container.RegisterInstance(mockChallengeService.Object).As<IChallengeService>().SingleInstance();
                        });
                });

            var recurringJob = new ChallengeRecurringJob(
                new ChallengeService.ChallengeServiceClient(challengesHost.CreateChannel()),
                new GameService.GameServiceClient(gamesHost.CreateChannel()));

            // Act
            await recurringJob.SynchronizeChallengesAsync(EnumGame.LeagueOfLegends);

            // Assert
            mockServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()),
                Times.Exactly(2));

            mockChallengeService.Verify(
                challengeService => challengeService.GetMatchesAsync(
                    Game.LeagueOfLegends,
                    It.IsAny<PlayerId>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<IImmutableSet<string>>()),
                Times.Exactly(4));

            mockLogger.Verify(Times.Exactly(8));
        }

        [Fact]
        public async Task Success()
        {
            // Arrange
            var challenges = CreateChallenges().ToList();

            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(
                    challengeService => challengeService.GetMatchesAsync(
                        Game.LeagueOfLegends,
                        It.IsAny<PlayerId>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<DateTime?>(),
                        It.IsAny<IImmutableSet<string>>()))
                .ReturnsAsync(CreateChallengeMatches().ToList())
                .Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            using var challengesHost = new ChallengesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>().SingleInstance();
                        });
                });

            challengesHost.Server.CleanupDbContext();

            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IChallengeRepository>();

                    repository.Create(challenges);

                    await repository.CommitAsync(false);
                });

            using var gamesHost = new GamesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockChallengeService.Object).As<IChallengeService>().SingleInstance();
                        });
                });

            var recurringJob = new ChallengeRecurringJob(
                new ChallengeService.ChallengeServiceClient(challengesHost.CreateChannel()),
                new GameService.GameServiceClient(gamesHost.CreateChannel()));

            // Act
            await recurringJob.SynchronizeChallengesAsync(EnumGame.LeagueOfLegends);

            // Assert
            mockServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()),
                Times.Exactly(2));

            mockChallengeService.Verify(
                challengeService => challengeService.GetMatchesAsync(
                    Game.LeagueOfLegends,
                    It.IsAny<PlayerId>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<IImmutableSet<string>>()),
                Times.Exactly(4));
        }

        [Fact(Skip = "League of Legends API")]
        public async Task Success2()
        {
            // Arrange
            using var gamesHost = new GamesHostFactory();

            var gamesServiceClient = new GameService.GameServiceClient(gamesHost.CreateChannel());

            var findChallengeScoringRequest = new FindChallengeScoringRequest
            {
                Game = EnumGame.LeagueOfLegends
            };

            var findChallengeScoringResponse = await gamesServiceClient.FindChallengeScoringAsync(findChallengeScoringRequest);

            var createdAt = new DateTimeProvider(
                new DateTime(
                    2019,
                    12,
                    30,
                    21,
                    9,
                    22,
                    DateTimeKind.Utc));

            var startedAt = new DateTimeProvider(
                new DateTime(
                    2019,
                    12,
                    30,
                    22,
                    42,
                    50,
                    DateTimeKind.Utc));

            var challenge = new Challenge(
                new ChallengeId(),
                new ChallengeName("TEST CHALLENGE"),
                Game.LeagueOfLegends,
                BestOf.One,
                Entries.Two,
                new ChallengeTimeline(createdAt, ChallengeDuration.OneDay),
                new Scoring(
                    findChallengeScoringResponse.Scoring.Items.OrderBy(scoring => scoring.Order)
                        .ToDictionary(scoring => scoring.StatName, scoring => scoring.StatWeighting)));

            var participant = new Participant(
                new ParticipantId(),
                new UserId(),
                PlayerId.Parse("V1R8S4W19KGdqSTn-rRO-pUGv6lfu2BkdVCaz_8wd-m6zw"),
                new DateTimeProvider(createdAt.DateTime + TimeSpan.FromMinutes(5)));

            challenge.Register(participant);

            challenge.Register(
                new Participant(
                    new ParticipantId(),
                    new UserId(),
                    new PlayerId(),
                    new DateTimeProvider(createdAt.DateTime + TimeSpan.FromMinutes(10))));

            challenge.Start(startedAt);

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            using var challengesHost = new ChallengesHostFactory().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            container.RegisterInstance(mockServiceBusPublisher.Object).As<IServiceBusPublisher>().SingleInstance();
                        });
                });

            challengesHost.Server.CleanupDbContext();

            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IChallengeRepository>();

                    repository.Create(challenge);

                    await repository.CommitAsync(false);
                });

            var recurringJob = new ChallengeRecurringJob(
                new ChallengeService.ChallengeServiceClient(challengesHost.CreateChannel()),
                new GameService.GameServiceClient(gamesHost.CreateChannel()));

            // Act
            await recurringJob.SynchronizeChallengesAsync(EnumGame.LeagueOfLegends);

            // Assert
            await challengesHost.Server.UsingScopeAsync(
                async scope =>
                {
                    var repository = scope.GetRequiredService<IChallengeRepository>();

                    var persistentChallenge = await repository.FindChallengeAsync(challenge.Id);

                    persistentChallenge.FindParticipant(participant.PlayerId).Matches.Should().HaveCount(2);
                });

            mockServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<ChallengeSynchronizedIntegrationEvent>()),
                Times.Exactly(1));
        }
    }
}
