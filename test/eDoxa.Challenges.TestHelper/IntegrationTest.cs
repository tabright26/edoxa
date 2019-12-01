// Filename: IntegrationTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Challenges.TestHelper
{
    public abstract class IntegrationTest : IClassFixture<TestHostFixture>, IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestHost = testHost;
            TestData = testData;
            TestMapper = testMapper.Instance;
        }

        protected TestHostFixture TestHost { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
