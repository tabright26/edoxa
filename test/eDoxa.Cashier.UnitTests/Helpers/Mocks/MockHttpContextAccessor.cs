// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Security.Claims;

using eDoxa.Seedwork.Security.Constants;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Cashier.UnitTests.Helpers.Mocks
{
    public sealed class MockHttpContextAccessor : Mock<IHttpContextAccessor>
    {
        public MockHttpContextAccessor()
        {
            this.Setup(accessor => accessor.HttpContext.User.Claims)
                .Returns(
                    new HashSet<Claim>
                    {
                        new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7"),
                        new Claim(CustomClaimTypes.StripeConnectAccountId, "acct_test"),
                        new Claim(CustomClaimTypes.StripeCustomerId, "cus_test")
                    }
                );
        }
    }
}
