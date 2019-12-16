// Filename: BundlesOptions.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Cashier.Api.Application
{
    public sealed class TransactionBundlesOptions
    {
        public IDictionary<string, HashSet<TransactionBundleOptions>> Deposit { get; set; }

        public IDictionary<string, HashSet<TransactionBundleOptions>> Withdrawal { get; set; }

        public sealed class TransactionBundleOptions
        {
            public decimal Amount { get; set; }

            public decimal Price { get; set; }
        }
    }
}
