// Filename: TestUserResourceFilterAttribute.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Domain.Services.Stripe.Filters.Attributes
{
    public sealed class TestUserResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting([NotNull] ResourceExecutingContext context)
        {
            var environment = context.HttpContext.RequestServices.GetService<IHostingEnvironment>();

            if (!environment.IsDevelopment())
            {
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (configuration.GetSection("Users")
                             .Get<List<string>>()
                             .Contains(context.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value))
            {
                context.Result = new BadRequestObjectResult("The current test user can not use the Stripe API.");
            }
        }

        public void OnResourceExecuted([NotNull] ResourceExecutedContext context)
        {
        }
    }
}
