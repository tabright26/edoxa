﻿// Filename: WithdrawMoneyResourceFilterAttribute.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eDoxa.Cashier.Domain.Services.Stripe.Filters.Attributes
{
    public sealed class WithdrawMoneyResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting([NotNull] ResourceExecutingContext context)
        {
            if (!context.HttpContext.User.HasClaim(claim => claim.Type == CustomClaimTypes.StripeBankAccountId))
            {
                context.Result = new BadRequestObjectResult("A bank account is required to withdrawal.");
            }
        }

        public void OnResourceExecuted([NotNull] ResourceExecutedContext context)
        {
        }
    }
}