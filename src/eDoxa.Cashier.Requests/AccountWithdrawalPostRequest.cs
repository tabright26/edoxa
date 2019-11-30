// Filename: AccountWithdrawalPostRequest.cs
// Date Created: 2019-11-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Requests
{
    [JsonObject]
    public sealed class AccountWithdrawalPostRequest
    {
        [JsonConstructor]
        public AccountWithdrawalPostRequest(decimal amount)
        {
            Amount = amount;
        }

        public AccountWithdrawalPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("amount")]
        public decimal Amount { get; private set; }
    }
}
