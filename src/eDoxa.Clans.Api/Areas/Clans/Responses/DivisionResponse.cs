// Filename: DivisionResponse.cs
// Date Created: 2019-10-31
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Clans.Api.Areas.Clans.Responses
{
    [JsonObject]
    public class DivisionResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("clanId")]
        public Guid ClanId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("members")]
        public ICollection<MemberResponse> Members { get; set; }


    }
}
