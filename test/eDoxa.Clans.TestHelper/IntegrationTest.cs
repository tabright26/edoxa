// Filename: IntegrationTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Clans.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Clans.TestHelper
{
    public abstract class IntegrationTest : IClassFixture<TestHostFixture>, IClassFixture<TestMapperFixture>
    {
        protected IntegrationTest(TestHostFixture testHost, TestMapperFixture testMapper)
        {
            TestHost = testHost;
            TestMapper = testMapper.Instance;
            TestMock = new TestMockFixture();
        }

        protected TestMockFixture TestMock { get; }

        protected TestHostFixture TestHost { get; }

        protected IMapper TestMapper { get; }
    }
}
