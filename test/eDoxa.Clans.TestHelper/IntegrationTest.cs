// Filename: IntegrationTestClass.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        }

        protected TestHostFixture TestHost { get; }

        protected IMapper TestMapper { get; }
    }
}
