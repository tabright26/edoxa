﻿// Filename: FakeUserFilter.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Seedwork.Testing.Mocks
{
    public sealed class MockAsyncClaimsPrincipalFilter : IAsyncActionFilter
    {
        public MockAsyncClaimsPrincipalFilter(IEnumerable<Claim> claims)
        {
            Claims = claims;
        }

        private IEnumerable<Claim> Claims { get; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(Claims));

            await next();
        }
    }
}
