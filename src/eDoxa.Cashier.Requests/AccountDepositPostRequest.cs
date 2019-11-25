// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Cashier.Requests
{
    [DataContract]
    public sealed class AccountDepositPostRequest
    {
        public AccountDepositPostRequest(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

#nullable disable
        public AccountDepositPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "currency")]
        public string Currency { get; private set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
