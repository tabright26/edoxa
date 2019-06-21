// Filename: PrizePoolViewModel.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Domain.ViewModels
{
    [JsonObject]
    public class PrizePoolViewModel
    {
        [JsonProperty("currency")]
        public CurrencyType Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
