// Filename: IntegrationTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Cashier.TestHelper
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
