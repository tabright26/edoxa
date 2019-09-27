﻿// Filename: ClanResponse.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using eDoxa.Organizations.Clans.Domain.Models;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses
{
    [JsonObject]
    public class MemberResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("clanId")]
        public Guid ClanId { get; set; }
    }
}
