// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Identity.TestHelper.Fixtures;

using Microsoft.Extensions.Options;

using Xunit;

namespace eDoxa.Identity.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>, IClassFixture<TestValidator>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator)
        {
            TestData = testData;
            TestMapper = testMapper.Instance;
            TestOptionsWrapper = testValidator.OptionsWrapper;
        }

        protected TestDataFixture TestData { get; }

        protected IMapper TestMapper { get; }

        protected OptionsWrapper<IdentityStaticOptions> TestOptionsWrapper { get; }
    }
}
