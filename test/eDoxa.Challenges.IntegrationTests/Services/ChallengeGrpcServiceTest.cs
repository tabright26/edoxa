// Filename: ChallengeGrpcServiceTest.cs
// Date Created: 2020-01-11
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Challenges.Responses;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Services
{
    public sealed class ChallengeGrpcServiceTest : IntegrationTest // TODO: INTEGRATION TESTS
    {
        public ChallengeGrpcServiceTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Fact]
        public async Task FetchChallengesAsync_ShouldBeOkResult()
        {
            // Arrange
            var factory = TestHost;

            var client = new ChallengeService.ChallengeServiceClient(factory.CreateChannel());

            var request = new FetchChallengesRequest
            {
                Game = EnumGame.LeagueOfLegends,
                State = EnumChallengeState.InProgress
            };

            // Act
            var response = await client.FetchChallengesAsync(request);

            // Assert
            response.Should().BeOfType<FetchChallengesResponse>();

            response.Challenges.Should().HaveCount(0);
        }
    }
}
