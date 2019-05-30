// Filename: EntryFeeDTO.cs
// Date Created: 2019-05-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class EntryFeeDTO
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public CurrencyType CurrencyType { get; set; }
    }
}
