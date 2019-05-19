// Filename: StripeCardDTO.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class StripeCardDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("exp_month")]
        public long ExpMonth { get; set; }

        [JsonProperty("exp_year")]
        public long ExpYear { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }
    }
}
