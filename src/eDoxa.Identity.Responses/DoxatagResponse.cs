// Filename: UserDoxatagResponse.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Identity.Responses
{
    [JsonObject]
    public class DoxatagResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
