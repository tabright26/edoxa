// Filename: ICashierSercurity.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

namespace eDoxa.Cashier.Application.Abstractions
{
    public interface ICashierSecurity
    {
        UserId UserId { get; }

        StripeAccountId StripeAccountId { get; }

        StripeBankAccountId StripeBankAccountId { get; }

        StripeCustomerId StripeCustomerId { get; }

        IEnumerable<string> Roles { get; }

        IEnumerable<string> Permissions { get; }

        bool HasStripeBankAccount();
    }
}