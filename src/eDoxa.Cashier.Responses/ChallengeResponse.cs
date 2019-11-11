// Filename: ChallengeResponse.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Responses
{
    [JsonObject]
    public class ChallengeResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("entryFee")]
        public EntryFeeResponse EntryFee { get; set; }

        [JsonProperty("payout")]
        public PayoutResponse Payout { get; set; }
    }
}
