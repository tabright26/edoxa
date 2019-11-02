// Filename: ChallengeGameScoringControllerGetAsyncTest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.TestHelper;
using eDoxa.Arena.Games.TestHelper.Fixtures;

namespace eDoxa.Arena.Games.IntegrationTests.Areas.Challenge.Controllers
{
    public sealed class ChallengeGameScoringControllerGetAsyncTest : IntegrationTest // GABRIEL: Integration Tests
    {
        public ChallengeGameScoringControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }
    }
}
