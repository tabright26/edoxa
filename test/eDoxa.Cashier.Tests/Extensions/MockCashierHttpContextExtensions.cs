// Filename: MockCashierHttpContextExtensions.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Security.Abstractions;

using Moq;

namespace eDoxa.Cashier.Tests.Extensions
{
    public static class MockCashierHttpContextExtensions
    {
        public static void SetupGetProperties(this Mock<ICashierHttpContext> mockCashierHttpContext)
        {
            mockCashierHttpContext.SetupGet(mock => mock.UserId).Returns(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));

            mockCashierHttpContext.SetupGet(mock => mock.StripeAccountId).Returns(new StripeAccountId("acct_we23weqi24o"));

            mockCashierHttpContext.SetupGet(mock => mock.StripeCustomerId).Returns(new StripeCustomerId("cus_we234qwei14o"));

            mockCashierHttpContext.SetupGet(mock => mock.StripeBankAccountId).Returns(new StripeBankAccountId("ba_we12334rTi24o"));
        }
    }
}
