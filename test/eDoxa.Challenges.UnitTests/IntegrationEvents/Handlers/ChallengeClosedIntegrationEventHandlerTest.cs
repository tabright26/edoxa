// Filename: ChallengeClosedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;

namespace eDoxa.Challenges.UnitTests.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandlerTest : UnitTest // GABRIEL: UNIT TESTS.
    {
        public ChallengeClosedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }
    }
}
