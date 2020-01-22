// Filename: ChallengeGrpcServiceTest.cs
// Date Created: 2020-01-11
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Responses;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Grpc.Core;

using IdentityModel;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Services
{
    public sealed class ChallengeGrpcServiceTest : IntegrationTest
    {
        public ChallengeGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Fact]
        public async Task FetchChallenges_ShouldBeOfTypeFetchChallengesResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);

            host.Server.CleanupDbContext();

            var request = new FetchChallengesRequest();

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.FetchChallengesAsync(request);

            //Assert
            response.Should().BeOfType<FetchChallengesResponse>();
        }

        [Fact]
        public void FindChallenge_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new FindChallengeRequest()
            {
                ChallengeId = new ChallengeId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.FindChallengeAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task FindChallenge_ShouldBeOfTypeFindChallengeResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Eight,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new FindChallengeRequest()
            {
                ChallengeId = challengeId
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.FindChallengeAsync(request);

            //Assert
            response.Should().BeOfType<FindChallengeResponse>();
        }

        [Fact]
        public void CreateChallenge_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new CreateChallengeRequest()
            {
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.CreateChallengeAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task CreateChallenge_ShouldBeOfTypeCreateChallengeResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();


            var request = new CreateChallengeRequest()
            {
                BestOf = 5,
                Duration = 5,
                Entries = 5,
                Game = EnumGame.LeagueOfLegends,
                Name = "test",
                Type = EnumChallengeType.All
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.CreateChallengeAsync(request);

            //Assert
            response.Should().BeOfType<CreateChallengeResponse>();
        }

        [Fact]
        public void SynchronizeChallenge_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new SynchronizeChallengeRequest()
            {
                ChallengeId = new ChallengeId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.SynchronizeChallengeAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task SynchronizeChallenge_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Eight,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new SynchronizeChallengeRequest()
            {
                ChallengeId = challengeId
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.SynchronizeChallengeAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task SynchronizeChallenge_ShouldBeOfTypeSynchronizeChallengeResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Two,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));
                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));
                    challenge.Start(new UtcNowDateTimeProvider());

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new SynchronizeChallengeRequest()
            {
                ChallengeId = challengeId
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.SynchronizeChallengeAsync(request);

            //Assert
            response.Should().BeOfType<SynchronizeChallengeResponse>();
        }

        [Fact]
        public void RegisterChallengeParticipant_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new RegisterChallengeParticipantRequest()
            {
                ChallengeId = new ChallengeId(),
                GamePlayerId = new PlayerId(),
                ParticipantId = new ParticipantId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RegisterChallengeParticipantAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RegisterChallengeParticipant_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Two,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));
                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new RegisterChallengeParticipantRequest()
            {
                ChallengeId = challengeId,
                GamePlayerId = new PlayerId(),
                ParticipantId = new ParticipantId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.RegisterChallengeParticipantAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task RegisterChallengeParticipant_ShouldBeOfTypeRegisterChallengeParticipantResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Two,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new RegisterChallengeParticipantRequest()
            {
                ChallengeId = challengeId,
                GamePlayerId = new PlayerId(),
                ParticipantId = new ParticipantId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.RegisterChallengeParticipantAsync(request);

            //Assert
            response.Should().BeOfType<RegisterChallengeParticipantResponse>();
        }

        [Fact]
        public void SnapshotChallengeParticipant_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new SnapshotChallengeParticipantRequest()
            {
                ChallengeId = new ChallengeId(),
                GamePlayerId = new PlayerId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.SnapshotChallengeParticipantAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task SnapshotChallengeParticipant_ShouldThrowFailedPreconditionRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Two,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));
                    challenge.Register(new Participant(new ParticipantId(), new UserId(), new PlayerId(), new UtcNowDateTimeProvider()));

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new SnapshotChallengeParticipantRequest()
            {
                ChallengeId = challengeId,
                GamePlayerId = new PlayerId()
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.SnapshotChallengeParticipantAsync(request));
            func.Should().Throw<RpcException>();
        }

        [Fact]
        public async Task SnapshotChallengeParticipant_ShouldBeOfTypeSnapshotChallengeParticipantResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";
            var challengeId = new ChallengeId();
            var playerId = new PlayerId();

            var claims = new[] { new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email) };
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();

                    var challenge = new Challenge(
                        challengeId,
                        new ChallengeName("test"),
                        Game.LeagueOfLegends,
                        BestOf.Five,
                        Entries.Two,
                        new ChallengeTimeline(new UtcNowDateTimeProvider(), ChallengeDuration.FiveDays),
                        new Scoring());

                    challenge.Register(new Participant(new ParticipantId(), new UserId(), playerId, new UtcNowDateTimeProvider()));

                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var request = new SnapshotChallengeParticipantRequest()
            {
                ChallengeId = challengeId,
                GamePlayerId = playerId
            };

            var client = new ChallengeService.ChallengeServiceClient(host.CreateChannel());

            // Act
            var response = await client.SnapshotChallengeParticipantAsync(request);

            //Assert
            response.Should().BeOfType<SnapshotChallengeParticipantResponse>();
        }
    }
}
