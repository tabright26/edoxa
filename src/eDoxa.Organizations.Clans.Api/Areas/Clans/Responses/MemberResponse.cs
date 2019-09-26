// Filename: ClanResponse.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Organizations.Clans.Domain.Models;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses
{
    [JsonObject]
    public class MemberResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("clan")]
        public Clan Clan { get; set; }

    }
}
