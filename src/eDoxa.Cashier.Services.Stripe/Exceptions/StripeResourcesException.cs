// Filename: StripeResourcesException.cs
// Date Created: 2019-05-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Services.Stripe.Exceptions
{
    public sealed class StripeResourcesException : Exception
    {
        public StripeResourcesException() : base("Stripe resources were not properly initialized when creating the user.")
        {
        }
    }
}
