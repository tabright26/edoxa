// Filename: MockHttpContextAccessorModule.cs
// Date Created: 2019-08-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;

using Autofac;

using Microsoft.AspNetCore.Http;

using Moq;

namespace eDoxa.Seedwork.Testing.Modules
{
    public sealed class MockHttpContextAccessorModule : Module
    {
        internal MockHttpContextAccessorModule(params Claim[] claims)
        {
            Claims = claims;
        }

        private IEnumerable<Claim> Claims { get; }

        protected override void Load(ContainerBuilder builder)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Setup(stripeService => stripeService.HttpContext.User.Claims).Returns(Claims);

            builder.RegisterInstance(mockHttpContextAccessor.Object).As<IHttpContextAccessor>();
        }
    }
}
