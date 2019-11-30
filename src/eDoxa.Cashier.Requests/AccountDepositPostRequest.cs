// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-11-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Requests
{
    [JsonObject]
    public sealed class AccountDepositPostRequest
    {
        [JsonConstructor]
        public AccountDepositPostRequest(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public AccountDepositPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("amount")]
        public decimal Amount { get; private set; }
    }
}
