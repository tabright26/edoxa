// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

using ClaimTypes = eDoxa.Seedwork.Security.ClaimTypes;

namespace eDoxa.Cashier.UnitTests.TestHelpers.Mocks
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
                        new Claim(ClaimTypes.StripeConnectAccountId, "acct_test"),
                        new Claim(ClaimTypes.StripeCustomerId, "cus_test")
                    })
                .Verifiable();
        }

        public void VerifyGet(Times times)
        {
            this.VerifyGet(accessor => accessor.HttpContext.User.Claims, times);
        }
    }
}
