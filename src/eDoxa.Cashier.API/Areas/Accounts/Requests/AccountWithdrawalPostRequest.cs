// Filename: AccountWithdrawalPostRequest.cs
// Date Created: 2019-08-27
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

        public AccountWithdrawalPostRequest()
        {
            // Required by Fluent Validation.
        }

        [DataMember(Name = "amount")]
        public decimal Amount { get; private set; }
    }
}
