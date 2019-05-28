// Filename: StripeErrorTypes.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Services.Stripe
{
    internal sealed class StripeErrorTypes
    {
        internal const string CardError = "card_error";
        internal const string InvalidRequestError = "invalid_request_error";
        internal const string ValidationError = "validation_error";
    }
}
