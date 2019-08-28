// Filename: ChallengeResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses
{
    [JsonObject]
    public class ChallengeResponse
    {
        [JsonProperty("entryFee")]
        public EntryFeeResponse EntryFee { get; set; }

        [JsonProperty("payout")]
        public PayoutResponse Payout { get; set; }
    }
}
