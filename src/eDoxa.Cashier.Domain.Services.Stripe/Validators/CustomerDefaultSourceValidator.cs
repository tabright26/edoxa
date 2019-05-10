// Filename: CustomerValidator.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Validators
{
    public sealed class CustomerDefaultSourceValidator
    {
        public void Validate(Customer customer)
        {
            if (customer.DefaultSource == null)
            {
                throw new InvalidOperationException("The customer default source payment is invalid. This customer doesn't have any default payment source.");
            }

            if (customer.DefaultSource.Object != "card")
            {
                throw new InvalidOperationException("The customer default source payment is invalid. Only credit card are accepted.");
            }
        }
    }
}