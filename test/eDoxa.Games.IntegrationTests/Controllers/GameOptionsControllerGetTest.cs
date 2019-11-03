// Filename: GameOptionsControllerGetTest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

namespace eDoxa.Games.IntegrationTests.Controllers
{
    public sealed class GameOptionsControllerGetTest : IntegrationTest // GABRIEL: Integration Tests
    {
        public GameOptionsControllerGetTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }
    }
}
