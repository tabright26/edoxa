// Filename: CreateTransactionRequest.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace eDoxa.Cashier.Requests
{
    [DataContract]
    public sealed class CreateTransactionRequest
    {
        public CreateTransactionRequest(string type, string currency, decimal amount, IDictionary<string, string> metadata = null)
        {
            Type = type;
            Currency = currency;
            Amount = amount;
            Metadata = metadata ?? new Dictionary<string, string>();
        }

        [DataMember(Name = "type")]
        public string Type { get; private set; }

        [DataMember(Name = "currency")]
        public string Currency { get; private set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }

        [DataMember(Name = "metadata")]
        public IDictionary<string, string> Metadata { get; private set; }
    }
}
