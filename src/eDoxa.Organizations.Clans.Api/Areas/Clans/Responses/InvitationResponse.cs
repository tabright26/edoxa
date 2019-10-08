﻿// Filename: ClanResponse.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Responses
{
    [JsonObject]
    public class InvitationResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("clanId")]
        public Guid ClanId { get; set; }
    }
}