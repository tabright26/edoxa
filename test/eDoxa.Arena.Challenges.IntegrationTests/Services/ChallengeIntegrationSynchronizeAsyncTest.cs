// Filename: ChallengeIntegrationSynchronizeAsyncTestClass.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    public sealed class ChallengeIntegrationSynchronizeAsyncTest : IntegrationTest
    {
        public ChallengeIntegrationSynchronizeAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        //// TODO: The method name must be written as a test scenario.
        //[Fact]
        //public async Task ShouldHaveCountFive()
        //{
        //    // Arrange
        //    var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.InProgress);

        //    var challenges = challengeFaker.FakeChallenges(5);

        //    var factory = TestApi.WithWebHostBuilder(
        //        x =>
        //        {
        //            x.ConfigureTestContainer<ContainerBuilder>(
        //                y =>
        //                {
        //                    var mock = new Mock<IGamesApiRefitClient>();

        //                    mock.Setup(t => t.GetMatchesAsync(It.IsAny<PlayerId>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).ReturnsAsync(new List<Match>());

        //                    y.RegisterInstance(mock.Object).As<IGamesApiRefitClient>().SingleInstance();
        //                });
        //        });

        //    factory.CreateClient();
        //    var testServer = factory.Server;
        //    testServer.CleanupDbContext();

        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
        //            challengeRepository.Create(challenges);
        //            await challengeRepository.CommitAsync();
        //        });

        //    // Act
        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var challengeService = scope.GetRequiredService<IChallengeService>();
        //            var synchronizedAt = new DateTimeProvider(DateTime.UtcNow);
        //            await challengeService.SynchronizeAsync(Game.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);
        //        });

        //    // Arrange
        //    await testServer.UsingScopeAsync(
        //        async scope =>
        //        {
        //            var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
        //            challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.InProgress);
        //            challenges.Should().HaveCount(5);
        //        });
        //}
    }
}
