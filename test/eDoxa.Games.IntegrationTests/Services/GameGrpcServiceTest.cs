﻿// Filename: GameGrpcServiceTest.cs
// Date Created: 2020-01-11
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Requests;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using Xunit;

namespace eDoxa.Games.IntegrationTests.Services
{
    public sealed class GameGrpcServiceTest : IntegrationTest // TODO: INTEGRATION TESTS
    {
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

        public GameGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Theory]
        [MemberData(nameof(Senarios))]
        public async Task FetchChallengeMatches(
            string playerId,
            DateTime startedAt,
            DateTime endedAt,
            int count
        )
        {
            TestHost.Server.CleanupDbContext();

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
    }
}
