// Filename: ControllerTest.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    public abstract class ControllerTest
    {
        protected ControllerTest(CashierApiFactory apiFactory, TestDataFixture testData)
        {
            ApiFactory = apiFactory;
            TestData = testData;
        }

        protected CashierApiFactory ApiFactory { get; }

        protected TestDataFixture TestData { get; }
    }
}
