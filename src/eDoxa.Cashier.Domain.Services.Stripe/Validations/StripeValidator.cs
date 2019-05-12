// Filename: StripeValidator.cs
// Date Created: 2019-05-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Validations
{
    public sealed class StripeValidator
    {
        public bool Validate(Customer customer, out ValidationResult result)
        {
            result = null;

            if (customer.DefaultSource == null)
            {
                result = new ValidationResult("The customer default source payment is invalid. This customer doesn't have any default payment source.");

                return false;
            }

            if (customer.DefaultSource.Object != "card")
            {
                result = new ValidationResult("The customer default source payment is invalid. Only credit card are accepted.");

                return false;
            }

            return true;
        }
    }
}