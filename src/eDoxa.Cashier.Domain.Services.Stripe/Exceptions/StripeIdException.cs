// Filename: StripeIdException.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Domain.Services.Stripe.Exceptions
{
    internal class StripeIdException : Exception
    {
        public StripeIdException(string stripeId, Type type) : base($"The '{stripeId}' is not a valid StripeId of type: {type.FullName}.")
        {
        }
    }
}
