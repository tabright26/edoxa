// Filename: ServiceTest.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.TestHelpers
{
    public abstract class IntegrationTestClass : IClassFixture<ArenaChallengeApiFactory>, IClassFixture<TestDataFixture>
    {
        protected IntegrationTestClass(ArenaChallengeApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected ArenaChallengeApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
