// Filename: EmailResponse.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public sealed class EmailResponse
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }
    }
}
