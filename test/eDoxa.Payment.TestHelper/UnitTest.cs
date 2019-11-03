// Filename: UnitTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        }

        protected IMapper TestMapper { get; }
    }
}
