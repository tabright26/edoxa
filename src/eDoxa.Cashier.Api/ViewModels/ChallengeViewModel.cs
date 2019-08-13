// Filename: ChallengeViewModel.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.ViewModels
{
    [JsonObject]
    public class ChallengeViewModel
    {
        [JsonProperty("entryFee")]
        public EntryFeeViewModel EntryFee { get; set; }

        [JsonProperty("payout")]
        public PayoutViewModel Payout { get; set; }
    }
}
