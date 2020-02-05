// Filename: ProblemDetailsOptionsExtensions.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Hellang.Middleware.ProblemDetails;

using PayPal;

namespace eDoxa.Paypal.Extensions
{
    public static class ProblemDetailsOptionsExtensions
    {
        public static void MapPaypalException(this ProblemDetailsOptions options)
        {
            options.Map<PayPalException>(exception => new ExceptionProblemDetails(exception));
        }
    }
}
