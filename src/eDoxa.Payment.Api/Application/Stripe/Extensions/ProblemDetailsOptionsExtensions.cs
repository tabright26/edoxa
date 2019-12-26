// Filename: ProblemDetailsOptionsExtensions.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Hellang.Middleware.ProblemDetails;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Extensions
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static void MapStripeException(this ProblemDetailsOptions options)
        {
            options.Map<StripeException>(
                exception => new ExceptionProblemDetails(exception)
                {
                    Title = exception.StripeError.Code,
                    Detail = exception.StripeError.Message,
                    Type = exception.StripeError.DocUrl,
                    Status = (int?) exception.HttpStatusCode
                });
        }
    }
}
