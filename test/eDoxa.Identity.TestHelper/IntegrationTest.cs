// Filename: IntegrationTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Identity.TestHelper
{
    public abstract class IntegrationTest : IClassFixture<TestHostFixture>, IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestHost = testHost;
            TestData = testData;
            TestMapper = testMapper.Instance;
            TestMock = new TestMockFixture();
        }

        protected TestMockFixture TestMock { get; }

        protected TestHostFixture TestHost { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
