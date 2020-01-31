// Filename: IntegrationTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Payment.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Payment.TestHelper
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
