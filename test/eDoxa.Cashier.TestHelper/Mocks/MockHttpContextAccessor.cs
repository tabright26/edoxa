// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Cashier.TestHelper.Mocks
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
                        new Claim(JwtClaimTypes.Email, "noreply@edoxa.gg")
                    })
                .Verifiable();
        }

        public void VerifyGet(Times times)
        {
            this.VerifyGet(accessor => accessor.HttpContext.User.Claims, times);
        }
    }
}
