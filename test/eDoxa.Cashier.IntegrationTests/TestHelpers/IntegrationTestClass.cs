// Filename: IntegrationTestClass.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Cashier.IntegrationTests.TestHelpers
{
    public abstract class IntegrationTestClass : IClassFixture<CashierApiFactory>, IClassFixture<TestDataFixture>
    {
        protected IntegrationTestClass(CashierApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected CashierApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
