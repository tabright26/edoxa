//// Filename: ChallengeServiceTest.cs
//// Date Created: 2019-11-20
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;

//using eDoxa.Challenges.Api.Application.Services;
//using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
//using eDoxa.Challenges.Domain.Repositories;
//using eDoxa.Challenges.TestHelper;
//using eDoxa.Challenges.TestHelper.Fixtures;
//using eDoxa.Seedwork.Domain;
//using eDoxa.Seedwork.Domain.Dtos;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Mocks;

//using FluentAssertions;

//using Moq;

//using Xunit;

//namespace eDoxa.Challenges.UnitTests.Areas.Challenges.Services
//{
//    public sealed class ChallengeServiceTest : UnitTest
//    {
//        public ChallengeServiceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
//        {
//        }

//        [Fact]
//        public async Task CreateChallengeAsync_ShouldBeOfTypeValidationResult()
//        {
//            // Arrange
//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            var scoreDictionnary = new Dictionary<string, float>
//            {
//                {"test1", 10},
//                {"test2", 50},
//                {"test3", 100}
//            };

//            mockGamesHttpClient.Setup(client => client.GetChallengeScoringAsync(It.IsAny<Game>())).ReturnsAsync(new ScoringDto(scoreDictionnary)).Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.Create(It.IsAny<Challenge>())).Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            var result = await challengeService.CreateChallengeAsync(
//                new ChallengeId(),
//                new ChallengeName("test"),
//                Game.LeagueOfLegends,
//                new BestOf(5),
//                new Entries(10),
//                new ChallengeDuration(
//                    new TimeSpan(
//                        1,
//                        0,
//                        0,
//                        0)),
//                new UtcNowDateTimeProvider());

//            // Assert
//            result.Should().BeOfType<DomainValidationResult>();
//            mockGamesHttpClient.Verify(client => client.GetChallengeScoringAsync(It.IsAny<Game>()), Times.Once);
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.Create(It.IsAny<Challenge>()), Times.Once);
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [Fact]
//        public async Task DeleteChallengeAsync()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenge = challengeFaker.FakeChallenge();

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.Delete(It.IsAny<Challenge>())).Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            await challengeService.DeleteChallengeAsync(challenge);

//            // Assert
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.Delete(It.IsAny<Challenge>()), Times.Once);
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [Fact]
//        public async Task FindChallengeAsync_ShouldBeOfTypeChallenge()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenge = challengeFaker.FakeChallenge();

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()))
//                .ReturnsAsync(challenge)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            var result = await challengeService.FindChallengeAsync(new ChallengeId());

//            // Assert
//            result.Should().BeOfType<Challenge>();
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
//        }

//        [Fact]
//        public async Task RegisterParticipantAsync_ShouldBeOfTypeValidationResult()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenge = challengeFaker.FakeChallenge();
//            var participantCount = challenge.Entries - challenge.Participants.Count;
//            participantCount -= 1;

//            for (var index = 0; index < participantCount; index++)
//            {
//                challenge.Register(
//                    new Participant(
//                        new ParticipantId(),
//                        new UserId(),
//                        PlayerId.Parse(Guid.NewGuid().ToString()),
//                        new UtcNowDateTimeProvider()));
//            }

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            var result = await challengeService.RegisterChallengeParticipantAsync(
//                challenge,
//                new UserId(),
//                new ParticipantId(),
//                new PlayerId(),
//                new UtcNowDateTimeProvider());

//            // Assert
//            result.Should().BeOfType<DomainValidationResult>();
//            mockChallengeRepository.Verify(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
//        }

//        [Fact]
//        public async Task RegisterParticipantAsync_WhenAlreadyRegistered_ShouldBeOfTypeValidationResultWithErrors()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenge = challengeFaker.FakeChallenge();

//            var userId = new UserId();

//            challenge.Register(
//                new Participant(
//                    new ParticipantId(),
//                    userId,
//                    PlayerId.Parse(Guid.NewGuid().ToString()),
//                    new UtcNowDateTimeProvider()));

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            var result = await challengeService.RegisterChallengeParticipantAsync(
//                challenge,
//                userId,
//                new ParticipantId(),
//                new PlayerId(),
//                new UtcNowDateTimeProvider());

//            // Assert
//            result.Should().BeOfType<DomainValidationResult>();
//            result.Errors.Should().NotBeEmpty();
//        }

//        [Fact]
//        public async Task RegisterParticipantAsync_WhenSoldOut_ShouldBeOfTypeValidationResultWithErrors()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenge = challengeFaker.FakeChallenge();
//            var participantCount = challenge.Entries - challenge.Participants.Count;

//            for (var index = 0; index < participantCount; index++)
//            {
//                challenge.Register(
//                    new Participant(
//                        new ParticipantId(),
//                        new UserId(),
//                        PlayerId.Parse(Guid.NewGuid().ToString()),
//                        new UtcNowDateTimeProvider()));
//            }

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            var result = await challengeService.RegisterChallengeParticipantAsync(
//                challenge,
//                new UserId(),
//                new ParticipantId(),
//                new PlayerId(),
//                new UtcNowDateTimeProvider());

//            // Assert
//            result.Should().BeOfType<DomainValidationResult>();
//            result.Errors.Should().NotBeEmpty();
//        }

//        [Fact]
//        public async Task SynchronizeAsync_ShouldBeVerified()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(86597858, Game.LeagueOfLegends, ChallengeState.InProgress);

//            var challenges = challengeFaker.FakeChallenges(5);

//            var synchronizedAt = new UtcNowDateTimeProvider();

//            foreach (var challenge in challenges)
//            {
//                challenge.Synchronize(synchronizedAt);
//            }

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
//                .ReturnsAsync(challenges)
//                .Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            mockGamesHttpClient
//                .Setup(
//                    challengeRepository => challengeRepository.GetChallengeMatchesAsync(
//                        It.IsAny<Game>(),
//                        It.IsAny<string>(),
//                        It.IsAny<DateTime?>(),
//                        It.IsAny<DateTime?>()))
//                .ReturnsAsync(new List<ChallengeMatch>())
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            await challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, synchronizedAt);

//            // Assert
//            challenges.Should().OnlyContain(challenge => challenge.Timeline == ChallengeState.InProgress);
//            challenges.Should().OnlyContain(challenge => challenge.SynchronizedAt.Value == synchronizedAt.DateTime);

//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
//                Times.Exactly(2));

//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
//                Times.Exactly(challenges.SelectMany(x => x.Participants).Count() + challenges.Count));
//        }

//        [Fact]
//        public async Task SynchronizeChallengesAsync_ShouldBeOfTypeValidationResult()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.InProgress);
//            var challenges = challengeFaker.FakeChallenges(5);

//            var scoreDictionnary = new Dictionary<string, double>
//            {
//                {"test1", 10},
//                {"test2", 50},
//                {"test3", 100}
//            };

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
//                .ReturnsAsync(challenges)
//                .Verifiable();

//            mockGamesHttpClient.Setup(
//                    client => client.GetChallengeMatchesAsync(
//                        It.IsAny<Game>(),
//                        It.IsAny<string>(),
//                        It.IsAny<DateTime>(),
//                        It.IsAny<DateTime>()))
//                .ReturnsAsync(
//                    new List<ChallengeMatch>
//                    {
//                        new ChallengeMatch("test1", new UtcNowDateTimeProvider(), scoreDictionnary),
//                        new ChallengeMatch("test2", new UtcNowDateTimeProvider(), scoreDictionnary),
//                        new ChallengeMatch("test3", new UtcNowDateTimeProvider(), scoreDictionnary)
//                    })
//                .Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            await challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, new UtcNowDateTimeProvider());

//            // Assert
//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
//                Times.Exactly(2));

//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
//                Times.Exactly(challenges.SelectMany(x => x.Participants).Count() + 1));
//        }

//        [Fact]
//        public async Task SynchronizeChallengesAsync_WhenChallengeIsBadState_ShouldLogError()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.Inscription);
//            var challenges = challengeFaker.FakeChallenges(5);

//            var scoreDictionnary = new Dictionary<string, double>
//            {
//                {"test1", 10},
//                {"test2", 50},
//                {"test3", 100}
//            };

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
//                .ReturnsAsync(challenges)
//                .Verifiable();

//            mockGamesHttpClient.Setup(
//                    client => client.GetChallengeMatchesAsync(
//                        It.IsAny<Game>(),
//                        It.IsAny<string>(),
//                        It.IsAny<DateTime>(),
//                        It.IsAny<DateTime>()))
//                .ReturnsAsync(
//                    new List<ChallengeMatch>
//                    {
//                        new ChallengeMatch("test1", new UtcNowDateTimeProvider(), scoreDictionnary),
//                        new ChallengeMatch("test2", new UtcNowDateTimeProvider(), scoreDictionnary),
//                        new ChallengeMatch("test3", new UtcNowDateTimeProvider(), scoreDictionnary)
//                    })
//                .Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            await challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, new UtcNowDateTimeProvider());

//            // Assert
//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
//                Times.Exactly(2));

//            mockLogger.Verify(Times.Exactly(5));
//        }

//        [Fact]
//        public async Task SynchronizeChallengesAsync_WhenParticipantMatchError_ShouldLogError()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(39042334, Game.LeagueOfLegends, ChallengeState.InProgress);
//            var challenges = challengeFaker.FakeChallenges(5);

//            var error = await ApiException.Create(new HttpRequestMessage(), HttpMethod.Patch, new HttpResponseMessage(HttpStatusCode.BadRequest));

//            var mockChallengeRepository = new Mock<IChallengeRepository>();
//            var mockGamesHttpClient = new Mock<IGamesHttpClient>();
//            var mockLogger = new MockLogger<ChallengeService>();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
//                .ReturnsAsync(challenges)
//                .Verifiable();

//            mockGamesHttpClient.Setup(
//                    client => client.GetChallengeMatchesAsync(
//                        It.IsAny<Game>(),
//                        It.IsAny<string>(),
//                        It.IsAny<DateTime>(),
//                        It.IsAny<DateTime>()))
//                .ThrowsAsync(error)
//                .Verifiable();

//            mockChallengeRepository.Setup(challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var challengeService = new ChallengeService(mockChallengeRepository.Object, mockGamesHttpClient.Object, mockLogger.Object);

//            // Act
//            await challengeService.SynchronizeChallengesAsync(Game.LeagueOfLegends, new UtcNowDateTimeProvider());

//            // Assert
//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
//                Times.Exactly(2));

//            mockChallengeRepository.Verify(
//                challengeRepository => challengeRepository.CommitAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()),
//                Times.Exactly(challenges.Count));

//            mockLogger.Verify(Times.Exactly(challenges.SelectMany(challenge => challenge.Participants).Count() - challenges.Count + 1));
//        }
//    }
//}
