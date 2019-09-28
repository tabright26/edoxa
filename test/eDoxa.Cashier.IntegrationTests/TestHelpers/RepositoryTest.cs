// Filename: RepositoryTest.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.IntegrationTests.TestHelpers
{
    public abstract class RepositoryTest
    {
        protected RepositoryTest(CashierApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected CashierApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
