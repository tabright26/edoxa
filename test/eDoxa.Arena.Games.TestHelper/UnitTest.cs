// Filename: UnitTest.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Games.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Arena.Games.TestHelper
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
