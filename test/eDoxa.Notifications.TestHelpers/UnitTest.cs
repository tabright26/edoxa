// Filename: UnitTest.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Notifications.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Notifications.TestHelpers
{
    public abstract class UnitTest : IClassFixture<TestMapperFixture>
    {
        protected UnitTest(TestMapperFixture testMapper)
        {
            TestMapper = testMapper.Instance;
        }

        protected IMapper TestMapper { get; }
    }
}
