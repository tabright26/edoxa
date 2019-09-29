// Filename: ControllerTest.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Identity.IntegrationTests.TestHelpers
{
    public abstract class IntegrationTestClass : IClassFixture<IdentityApiFactory>, IClassFixture<TestDataFixture>
    {
        protected IntegrationTestClass(IdentityApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected IdentityApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
