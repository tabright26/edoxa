// Filename: ClanResponse.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Clans.Responses
{
    [JsonObject]
    public class ClanResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("ownerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("members")]
        public ICollection<MemberResponse> Members { get; set; }

        [JsonProperty("divisions")]
        public ICollection<DivisionResponse> Divisions { get; set; }
    }
}
