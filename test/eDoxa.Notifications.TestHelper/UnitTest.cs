// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Notifications.TestHelper.Fixtures;

using Xunit;

namespace eDoxa.Notifications.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestMapperFixture>
    {
        protected UnitTest(TestMapperFixture testMapper)
        {
            TestMapper = testMapper.Instance;
            TestMock = new TestMockFixture();
        }

        protected IMapper TestMapper { get; }

        protected TestMockFixture TestMock { get; }
    }
}
