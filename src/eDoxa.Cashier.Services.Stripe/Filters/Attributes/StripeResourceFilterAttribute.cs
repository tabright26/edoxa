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
using System.Linq;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Services.Stripe.Filters.Attributes
{
    public sealed class StripeResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting([NotNull] ResourceExecutingContext context)
        {
            var environment = context.HttpContext.RequestServices.GetService<IHostingEnvironment>();

            if (!environment.IsDevelopment())
            {
                return;
            }

            if (context.HttpContext.User.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value == "e4655fe0-affd-4323-b022-bdb2ebde6091")
            {
                context.Result = new BadRequestObjectResult("The current test user can not use the Stripe API.");
            }
        }

        public void OnResourceExecuted([NotNull] ResourceExecutedContext context)
        {
        }
    }
}
