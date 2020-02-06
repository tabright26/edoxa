// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Games.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Games.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper)
        {
            TestData = testData;
            TestMapper = testMapper.Instance;
            TestMock = new TestMockFixture();
        }

        protected TestMockFixture TestMock { get; }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }
    }
}
