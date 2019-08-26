// Filename: DepositRequest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using MediatR;

namespace eDoxa.Cashier.Api.Application.Requests
{
    [DataContract]
    public sealed class DepositRequest : IRequest
    {
        public DepositRequest(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

#nullable disable
        public DepositRequest()
        {
            // Required by Fluent Validation
        }
#nullable restore

        [DataMember(Name = "currency")]
        public string Currency { get; private set; }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
