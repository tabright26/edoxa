// Filename: ChallengeGrpcServiceTest.cs
// Date Created: 2020-01-08
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Services;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Enums;
using eDoxa.Grpc.Protos.Challenges.Requests;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Grpc.Core;
using Grpc.Core.Testing;
using Grpc.Core.Utils;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Services
{
    public sealed class ChallengeGrpcServiceTest : UnitTest
    {
        public ChallengeGrpcServiceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        [Fact]
        public async Task FetchChallengesAsync_ShouldBeOkResult()
        {
            // Arrange
            var request = new FetchChallengesRequest
            {
                Game = GameDto.LeagueOfLegends,
                State = ChallengeStateDto.InProgress
            };

            var fakeServerCallContext = TestServerCallContext.Create(
                "test",
                null,
                DateTime.UtcNow,
                new Metadata(),
                CancellationToken.None,
                "127.0.0.1",
                null,
                null,
                metadata => TaskUtils.CompletedTask,
                () => new WriteOptions(),
                writeOptions =>
                {
                });

            var mockChallengeService = new Mock<IChallengeService>();
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockServiceBus = new Mock<IServiceBusPublisher>();

            mockChallengeQuery.Setup(challenges => challenges.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            var service = new ChallengeGrpcService(mockChallengeService.Object, mockChallengeQuery.Object, mockServiceBus.Object);

            // Act
            var result = await service.FetchChallenges(request, fakeServerCallContext);

            // Assert
            result.Should().BeOfType<OkResult>();

            mockChallengeQuery.Verify(challenges => challenges.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()), Times.Once);
        }

    }
}
