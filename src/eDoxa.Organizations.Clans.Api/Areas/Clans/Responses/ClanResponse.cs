// Filename: ClanResponse.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Collections.Generic;

using eDoxa.Organizations.Clans.Domain.Models;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses
{
    [JsonObject]
    public class ClanResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("ownerId")]
        public Guid OwnerId { get; set; }

        [JsonProperty("members")]
        public ICollection<Member> Members { get; set; }
    }
}
