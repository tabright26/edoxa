// Filename: MockCashierSecurityExtensions.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

using Moq;

namespace eDoxa.Cashier.Tests.Extensions
{
    public static class MockCashierSecurityExtensions
    {
        public static void SetupGetProperties(this Mock<ICashierSecurity> mockUserInfoService)
        {
            mockUserInfoService.SetupGet(mock => mock.UserId).Returns(UserId.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));

            mockUserInfoService.SetupGet(mock => mock.StripeAccountId).Returns(new StripeAccountId("acct_we23weqi24o"));

            mockUserInfoService.SetupGet(mock => mock.StripeCustomerId).Returns(new StripeCustomerId("cus_we234qwei14o"));

            mockUserInfoService.SetupGet(mock => mock.StripeBankAccountId).Returns(new StripeBankAccountId("ba_we12334rTi24o"));

            mockUserInfoService.SetupGet(mock => mock.Roles).Returns(new[]
            {
                "Role1",
                "Role2",
                "Role3",
                "Role4",
                "Role5"
            });

            mockUserInfoService.SetupGet(mock => mock.Permissions).Returns(new[]
            {
                "permission1",
                "permission2",
                "permission3",
                "permission4",
                "permission5"
            });

            mockUserInfoService.Setup(mock => mock.HasStripeBankAccount()).Returns(true);
        }
    }
}