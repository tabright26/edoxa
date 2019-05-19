// Filename: StripeExceptionFilter.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Filters
{
    public sealed class StripeExceptionFilter : IExceptionFilter
    {
        public void OnException([NotNull] ExceptionContext context)
        {
            if (context.Exception is StripeException exception)
            {
                if (exception.StripeError.ErrorType == StripeErrorTypes.ValidationError ||
                    exception.StripeError.ErrorType == StripeErrorTypes.InvalidRequestError ||
                    exception.StripeError.ErrorType == StripeErrorTypes.CardError)
                {
                    context.Result = new BadRequestObjectResult(exception.Message);
                }
            }
        }
    }
}
