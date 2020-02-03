// Filename: UnitTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Options;

using Microsoft.Extensions.Options;

using Xunit;

namespace eDoxa.Cashier.TestHelper
{
    public abstract class UnitTest : IClassFixture<TestDataFixture>, IClassFixture<TestMapperFixture>, IClassFixture<TestValidator>, IClassFixture<TestMockFixture>
    {
        protected UnitTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator)
        {
            TestData = testData;
            TestMock = new TestMockFixture();
            TestValidator = testValidator.OptionsWrapper;
            TestMapper = testMapper.Instance;
        }

        protected TestDataFixture TestData { get; }

        protected TestMockFixture TestMock { get; }

        protected IOptionsSnapshot<CashierApiOptions> TestValidator { get; }

        protected IMapper TestMapper { get; }
    }
}
