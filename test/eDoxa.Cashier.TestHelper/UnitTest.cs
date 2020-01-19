// Filename: UnitTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Options;

using Microsoft.Extensions.Options;

using Xunit;

namespace eDoxa.Cashier.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>, IClassFixture<TestValidator>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator)
        {
            TestData = testData;
            TestValidator = testValidator.OptionsWrapper;
            TestMapper = testMapper.Instance;
        }

        protected TestDataFixture TestData { get; }

        protected IOptionsSnapshot<CashierApiOptions> TestValidator { get; }

        protected IMapper TestMapper { get; }
    }
}
