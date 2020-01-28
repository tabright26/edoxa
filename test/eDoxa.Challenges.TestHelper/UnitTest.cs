// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Challenges.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>, IClassFixture<TestValidator>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator)
        {
            TestData = testData;
            TestValidator = testValidator;
            TestMapper = testMapper.Instance;
        }

        protected TestDataFixture TestData { get; }

        public TestValidator TestValidator { get; }

        protected IMapper TestMapper { get; }
    }
}
