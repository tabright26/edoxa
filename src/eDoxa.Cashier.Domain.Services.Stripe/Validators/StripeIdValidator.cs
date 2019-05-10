﻿// Filename: StripeIdValidator.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

namespace eDoxa.Cashier.Domain.Services.Stripe.Validators
{
    public sealed class StripeIdValidator
    {
        public void Validate(string stripeId, string stripePrefix)
        {
            var substrings = stripeId.Split('_');

            if (substrings.Length != 2)
            {
                throw new FormatException("The substrings of identity are in an incorrect format.");
            }

            var prefix = substrings[0];

            var suffix = substrings[1];

            if (prefix != stripePrefix)
            {
                throw new FormatException($"The identity prefix ({prefix}) is ​​an incorrect format.");
            }

            if (!suffix.All(char.IsLetterOrDigit))
            {
                throw new FormatException($"The identity suffix ({suffix}) is ​​an incorrect format.");
            }
        }
    }
}