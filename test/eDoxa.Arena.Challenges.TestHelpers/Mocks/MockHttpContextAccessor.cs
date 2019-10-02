// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Arena.Challenges.TestHelpers.Mocks
{
    public sealed class MockHttpContextAccessor : Mock<IHttpContextAccessor>
    {
        public MockHttpContextAccessor()
        {
            this.Setup(accessor => accessor.HttpContext.User.Claims).Returns(new[] {new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7")});
        }
    }
}
