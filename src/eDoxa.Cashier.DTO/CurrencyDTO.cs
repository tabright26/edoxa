// Filename: CurrencyDTO.cs
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
    public class CurrencyDTO
    {
        [JsonProperty("type")]
        public CurrencyType Type { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
