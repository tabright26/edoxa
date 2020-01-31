// Filename: GameGrpcServiceTest.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Responses;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using IdentityModel;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Grpc.Services
{
    public sealed class GameGrpcServiceTest : IntegrationTest
    {
        public GameGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        public static TheoryData<string, DateTime, DateTime, int> Senarios = new TheoryData<string, DateTime, DateTime, int>
        {
            {
                "V1R8S4W19KGdqSTn-rRO-pUGv6lfu2BkdVCaz_8wd-m6zw", new DateTime(
                    2019,
                    12,
                    30,
                    22,
                    42,
                    50,
                    DateTimeKind.Utc),
                new DateTime(
                    2019,
                    12,
                    31,
                    23,
                    00,
                    09,
                    DateTimeKind.Utc),
                2
            },
            {
                "sHTdp0lgHGiLBLad-8Sua63TpYJid-xsALG1DaRbPwVr6Q", new DateTime(
                    2020,
                    1,
                    4,
                    22,
                    50,
                    5,
                    DateTimeKind.Utc),
                new DateTime(
                    2020,
                    1,
                    5,
                    22,
                    50,
                    5,
                    DateTimeKind.Utc),
                1
            }
        };

        [Theory(Skip = "Real api calls")]
        [MemberData(nameof(Senarios))]
        public async Task FetchChallengeMatches(
            string playerId,
            DateTime startedAt,
            DateTime endedAt,
            int count
        )
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var client = new GameService.GameServiceClient(TestHost.CreateChannel());

            var matches = new List<GameMatchDto>();

            await foreach (var fetchChallengeMatchesResponse in client.FetchChallengeMatches(
                    new FetchChallengeMatchesRequest
                    {
                        Game = EnumGame.LeagueOfLegends,
                        StartedAt = startedAt.ToTimestamp(),
                        EndedAt = endedAt.ToTimestamp(),
                        Participants =
                        {
                            new ParticipantDto
                            {
                                Id = new ParticipantId(),
                                GamePlayerId = playerId
                            }
                        }
                    })
                .ResponseStream.ReadAllAsync())
            {
                matches.AddRange(fetchChallengeMatchesResponse.Matches);
            }

            matches.Should().HaveCount(count);
        }

        [Fact]
        public void FetchChallengeMatches_ShouldBeOfTypeFetchChallengeMatchesResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new FetchChallengeMatchesRequest
            {
                Game = EnumGame.LeagueOfLegends,
                EndedAt = DateTime.UtcNow.ToTimestamp(),
                StartedAt = (DateTime.UtcNow - TimeSpan.FromDays(1)).ToTimestamp(),
                Participants =
                {
                    new ParticipantDto
                    {
                        ChallengeId = new ChallengeId(),
                        GamePlayerId = "testID1",
                        Id = new Guid().ToString(),
                        Score = 10,
                        SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                        UserId = new UserId()
                    },
                    new ParticipantDto
                    {
                        ChallengeId = new ChallengeId(),
                        GamePlayerId = "testID2",
                        Id = new Guid().ToString(),
                        Score = 20,
                        SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                        UserId = new UserId()
                    },
                    new ParticipantDto
                    {
                        ChallengeId = new ChallengeId(),
                        GamePlayerId = "testID3",
                        Id = new Guid().ToString(),
                        Score = 50,
                        SynchronizedAt = DateTime.UtcNow.ToTimestamp(),
                        UserId = new UserId()
                    }
                }
            };

            var client = new GameService.GameServiceClient(host.CreateChannel());

            // Act
            var response = client.FetchChallengeMatches(request);

            //Assert
            response.Should().BeOfType<AsyncServerStreamingCall<FetchChallengeMatchesResponse>>();
        }

        [Fact]
        public async Task FetchChallengeScoring_ShouldBeOfTypeFetchChallengeScoringResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new FindChallengeScoringRequest
            {
                Game = EnumGame.LeagueOfLegends,
                ChallengeType = EnumChallengeType.All
            };

            var client = new GameService.GameServiceClient(host.CreateChannel());

            // Act
            var response = await client.FindChallengeScoringAsync(request);

            //Assert
            response.Should().BeOfType<FindChallengeScoringResponse>();
        }

        [Fact]
        public async Task FindPlayerGameCredential_ShouldBeOfTypeFindPlayerGameCredentialResponse()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            await host.Server.UsingScopeAsync(
                async scope =>
                {
                    var credentialRepository = scope.GetRequiredService<IGameCredentialRepository>();

                    var credential = new Credential(
                        userId,
                        Game.LeagueOfLegends,
                        new PlayerId(),
                        new UtcNowDateTimeProvider());

                    credentialRepository.CreateCredential(credential);

                    await credentialRepository.UnitOfWork.CommitAsync();
                });

            var request = new FindPlayerGameCredentialRequest
            {
                Game = EnumGame.LeagueOfLegends
            };

            var client = new GameService.GameServiceClient(host.CreateChannel());

            // Act
            var response = await client.FindPlayerGameCredentialAsync(request);

            //Assert
            response.Should().BeOfType<FindPlayerGameCredentialResponse>();
        }

        [Fact]
        public void FindPlayerGameCredential_ShouldThrowNotFoundRpcException()
        {
            // Arrange
            var userId = new UserId();
            const string email = "test@edoxa.gg";

            var claims = new[] {new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim(JwtClaimTypes.Email, email)};
            var host = TestHost.WithClaimsFromBearerAuthentication(claims);
            host.Server.CleanupDbContext();

            var request = new FindPlayerGameCredentialRequest
            {
                Game = EnumGame.LeagueOfLegends
            };

            var client = new GameService.GameServiceClient(host.CreateChannel());

            // Act Assert
            var func = new Func<Task>(async () => await client.FindPlayerGameCredentialAsync(request));
            func.Should().Throw<RpcException>();
        }
    }
}
