// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Requests
{
    [JsonObject]
    public sealed class CreateChallengeRequest
    {
        [JsonConstructor]
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

        public CreateChallengeRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("challengeId")]
        public Guid ChallengeId { get; private set; }

        [JsonProperty("payoutEntries")]
        public int PayoutEntries { get; private set; }

        [JsonProperty("entryFeeAmount")]
        public decimal EntryFeeAmount { get; private set; }

        [JsonProperty("entryFeeCurrency")]
        public string EntryFeeCurrency { get; private set; }
    }
}
