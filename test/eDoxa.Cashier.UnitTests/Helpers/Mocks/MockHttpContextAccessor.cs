// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using eDoxa.Seedwork.Security;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Cashier.UnitTests.Helpers.Mocks
{
    public sealed class MockHttpContextAccessor : Mock<IHttpContextAccessor>
    {
        public MockHttpContextAccessor()
        {
            this.SetupGet(accessor => accessor.HttpContext.User.Claims)
                .Returns(
                    new HashSet<Claim>
                    {
                        new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7"),
                        new Claim(AppClaimTypes.StripeConnectAccountId, "acct_test"),
                        new Claim(AppClaimTypes.StripeCustomerId, "cus_test")
                    }
                ).Verifiable();
        }

        public void VerifyGet(Times times)
        {
            this.VerifyGet(accessor => accessor.HttpContext.User.Claims, times);
        }
    }
}
