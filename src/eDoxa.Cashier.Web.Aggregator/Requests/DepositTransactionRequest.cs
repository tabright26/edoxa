// Filename: DepositTransactionRequest.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Cashier.Web.Aggregator.Requests
{
    public class DepositTransactionRequest
    {
        public int BundleId { get; set; }

        public string PaymentMethodId { get; set; }
    }
}
