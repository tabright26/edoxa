// Filename: MockHttpContextAccessor.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;

using eDoxa.Seedwork.Testing.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Helpers.Mocks
{
    public sealed class MockHttpContextAccessor : Mock<IHttpContextAccessor>
    {
        public MockHttpContextAccessor()
        {
            this.Setup(accessor => accessor.HttpContext.User.Claims).Returns(new Claim(JwtClaimTypes.Subject, "5C43502B-FCE8-4235-8557-C22D2A638AD7").ToList());
        }
    }
}
