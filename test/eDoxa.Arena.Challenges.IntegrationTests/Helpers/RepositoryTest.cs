// Filename: RepositoryTest.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    public abstract class RepositoryTest
    {
        protected RepositoryTest(ArenaChallengeApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected ArenaChallengeApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
