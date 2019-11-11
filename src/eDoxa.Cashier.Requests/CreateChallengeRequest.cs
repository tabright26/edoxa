// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace eDoxa.Cashier.Requests
{
    [DataContract]
    public sealed class CreateChallengeRequest
    {
        public CreateChallengeRequest(
            Guid challengeId,
            int payoutEntries,
            decimal entryFeeAmount,
            string entryFeeCurrency
        )
        {
            ChallengeId = challengeId;
            PayoutEntries = payoutEntries;
            EntryFeeAmount = entryFeeAmount;
            EntryFeeCurrency = entryFeeCurrency;
        }

        [DataMember(Name = "challengeId")]
        public Guid ChallengeId { get; private set; }

        [DataMember(Name = "payoutEntries")]
        public int PayoutEntries { get; private set; }

        [DataMember(Name = "entryFeeAmount")]
        public decimal EntryFeeAmount { get; private set; }

        [DataMember(Name = "entryFeeCurrency")]
        public string EntryFeeCurrency { get; private set; }
    }
}
