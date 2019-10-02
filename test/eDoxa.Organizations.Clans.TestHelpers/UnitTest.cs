// Filename: UnitTestClass.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Organizations.Clans.TestHelpers.Fixtures;

using Xunit;

namespace eDoxa.Organizations.Clans.TestHelpers
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
