// Filename: ChallengeGameMatchesControllerGetAsyncTest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

namespace eDoxa.Games.IntegrationTests.Areas.Challenge.Controllers
{
    public sealed class ChallengeGameMatchesControllerGetAsyncTest : IntegrationTest // GABRIEL: Integration Tests
    {
        public ChallengeGameMatchesControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }
    }
}
