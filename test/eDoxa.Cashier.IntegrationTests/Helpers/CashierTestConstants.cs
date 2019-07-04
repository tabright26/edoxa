// Filename: CashierTestConstants.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    internal static class CashierTestConstants
    {
        public const string TestStripeCustomerId = "cus_test";
        public const string TestStripeConnectAccountId = "acct_test";

        public static readonly UserId TestUserId = UserId.Parse("2a9128cc-0aa0-4f43-952f-d615debd7bd1");
    }
}
