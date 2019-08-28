// Filename: WithdrawalRequest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Cashier.Api.Areas.Accounts.Requests
{
    [DataContract]
    public sealed class AccountWithdrawalPostRequest
    {
        public AccountWithdrawalPostRequest(decimal amount)
        {
            Amount = amount;
        }

#nullable disable
        public AccountWithdrawalPostRequest()
        {
            // Required by Fluent Validation
        }
#nullable restore

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
