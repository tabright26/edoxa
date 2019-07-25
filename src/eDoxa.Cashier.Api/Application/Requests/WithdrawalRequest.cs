// Filename: WithdrawalRequest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using MediatR;

namespace eDoxa.Cashier.Api.Application.Requests
{
    [DataContract]
    public sealed class WithdrawalRequest : IRequest
    {
        public WithdrawalRequest(decimal amount)
        {
            Amount = amount;
        }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
