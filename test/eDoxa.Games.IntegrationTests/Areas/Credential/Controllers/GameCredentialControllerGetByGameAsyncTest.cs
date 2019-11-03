// Filename: GameCredentialControllerGetByGameAsyncTest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;

namespace eDoxa.Games.IntegrationTests.Areas.Credential.Controllers
{
    public sealed class GameCredentialControllerGetByGameAsyncTest : IntegrationTest // GABRIEL: Integration Tests
    {
        public GameCredentialControllerGetByGameAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }
    }
}
