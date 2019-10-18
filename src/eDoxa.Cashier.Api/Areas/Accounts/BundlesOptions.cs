// Filename: BundlesOptions.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;
using System.Collections.Immutable;

namespace eDoxa.Cashier.Api.Areas.Accounts
{
    public sealed class BundlesOptions
    {
        public IDictionary<string, HashSet<BundleOptions>> Deposit { get; set; }

        public IDictionary<string, HashSet<BundleOptions>> Withdrawal { get; set; }

        public sealed class BundleOptions
        {
            public decimal Amount { get; set; }

            public decimal Price { get; set; }
        }
    }
}
