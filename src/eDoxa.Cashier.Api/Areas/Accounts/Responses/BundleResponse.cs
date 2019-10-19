// Filename: BundleResponse.cs
// Date Created: 2019-10-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.Areas.Accounts.Responses
{
    [JsonObject]
    public sealed class BundleResponse
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
