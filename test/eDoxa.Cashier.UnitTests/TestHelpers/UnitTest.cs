// Filename: UnitTest.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Xunit;

namespace eDoxa.Cashier.UnitTests.TestHelpers
{
    public abstract class UnitTest : IClassFixture<CashierFakerFixture>
    {
        protected UnitTest(CashierFakerFixture faker)
        {
            Faker = faker;
        }

        protected CashierFakerFixture Faker { get; }
    }
}
