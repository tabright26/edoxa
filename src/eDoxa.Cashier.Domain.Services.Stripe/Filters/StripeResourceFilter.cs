// Filename: StripeResourceFilter.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Services.Stripe.Exceptions;
using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Cashier.Domain.Services.Stripe.Filters
{
    public sealed class StripeResourceFilter : IResourceFilter
    {
        public void OnResourceExecuting([NotNull] ResourceExecutingContext context)
        {
            if (!context.HttpContext.User.HasClaim(claim => claim.Type == CustomClaimTypes.StripeAccountId) ||
                !context.HttpContext.User.HasClaim(claim => claim.Type == CustomClaimTypes.StripeCustomerId))
            {
                throw new StripeResourcesException();
            }
        }

        public void OnResourceExecuted([NotNull] ResourceExecutedContext context)
        {
        }
    }
}
