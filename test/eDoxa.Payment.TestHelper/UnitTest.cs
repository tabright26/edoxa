// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Payment.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Payment.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestMapperFixture>
    {
        protected UnitTest(TestMapperFixture testMapper)
        {
            TestMapper = testMapper.Instance;
            TestMock = new TestMockFixture();
        }

        protected TestMockFixture TestMock { get; }

        protected IMapper TestMapper { get; }
    }
}
