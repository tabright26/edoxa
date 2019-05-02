// Filename: CustomApiResources.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using IdentityServer4.Models;

namespace eDoxa.IS
{
    public sealed class CustomApiResources
    {
        public sealed class IdentityApi : ApiResource
        {
            public IdentityApi() : base("edoxa.identity.api", "eDoxa Identity API")
            {
            }
        }

        public sealed class CashierApi : ApiResource
        {
            public CashierApi() : base("edoxa.cashier.api", "eDoxa Cashier API")
            {
            }
        }

        public sealed class ChallengeApi : ApiResource
        {
            public ChallengeApi() : base("edoxa.challenge.api", "eDoxa Challenge API")
            {
            }
        }
    }
}