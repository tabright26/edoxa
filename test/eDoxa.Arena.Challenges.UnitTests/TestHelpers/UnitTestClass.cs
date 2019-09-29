// Filename: UnitTestClass.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.TestHelpers
{
    public abstract class UnitTestClass : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected UnitTestClass(TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestData = testData;
            TestMapper = testMapper.Instance;
        }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
