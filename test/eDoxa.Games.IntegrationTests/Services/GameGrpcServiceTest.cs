// Filename: GameGrpcServiceTest.cs
// Date Created: 2020-01-03
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
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
    public sealed class GameGrpcServiceTest : IntegrationTest
    {
        public GameGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Fact]
        public async Task Test()
        {
            using var gamesHost = TestHost;

            //.WithWebHostBuilder(
            //builder => builder.ConfigureTestContainer<ContainerBuilder>(
            //    container =>
            //    {
            //        container.RegisterInstance(
            //                new LeagueOfLegendsService(
            //                    new OptionsWrapper<LeagueOfLegendsOptions>(
            //                        new LeagueOfLegendsOptions
            //                        {
            //                            ApiKey = "RGAPI-fb709a2b-1ecb-4d20-95fa-a4c598ce29e3"// TODO: Security.
            //                        })))
            //            .As<ILeagueOfLegendsService>()
            //            .SingleInstance();
            //    }));

            gamesHost.Server.CleanupDbContext();

            var client = new GameService.GameServiceClient(gamesHost.CreateChannel());

            var matches = new List<GameMatchDto>();

            await foreach (var fetchChallengeMatchesResponse in client.FetchChallengeMatches(
                    new FetchChallengeMatchesRequest
                    {
                        Game = GameDto.LeagueOfLegends,
                        Participants =
                        {
                            new GameParticipantDto
                            {
                                Id = new ParticipantId(),
                                PlayerId = "V1R8S4W19KGdqSTn-rRO-pUGv6lfu2BkdVCaz_8wd-m6zw",

                                //12/30/2019 10:42:50 PM
                                StartedAt = Timestamp.FromDateTime(
                                    new DateTime(
                                        2019,
                                        12,
                                        30,
                                        22,
                                        42,
                                        50,
                                        DateTimeKind.Utc)),

                                //12/31/2019 11:00:09 PM
                                EndedAt = Timestamp.FromDateTime(
                                    new DateTime(
                                        2019,
                                        12,
                                        31,
                                        23,
                                        00,
                                        09,
                                        DateTimeKind.Utc))
                            }
                        }
                    })
                .ResponseStream.ReadAllAsync())
            {
                matches.AddRange(fetchChallengeMatchesResponse.Matches);
            }

            matches.Count.Should().Be(2);
        }
    }
}
