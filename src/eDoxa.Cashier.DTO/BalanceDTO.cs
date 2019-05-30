// Filename: BalanceDTO.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class BalanceDTO
    {
        [JsonProperty("currency")]
        public CurrencyType CurrencyType { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("pending")]
        public decimal Pending { get; set; }
    }
}
