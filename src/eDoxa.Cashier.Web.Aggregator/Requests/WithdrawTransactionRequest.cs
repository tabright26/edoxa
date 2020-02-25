// Filename: WithdrawTransactionRequest.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Cashier.Web.Aggregator.Requests
{
    public class WithdrawTransactionRequest
    {
        public int BundleId { get; set; }

        public string Email { get; set; }
    }
}
