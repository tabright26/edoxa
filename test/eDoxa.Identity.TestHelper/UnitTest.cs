// Filename: UnitTestClass.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Identity.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Identity.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestData = testData;
            TestMapper = testMapper.Instance;
        }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
