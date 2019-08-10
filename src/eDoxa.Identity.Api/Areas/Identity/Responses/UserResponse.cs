// Filename: UserResponse.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public class UserResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("doxatag")]
        public DoxatagResponse Doxatag { get; set; }
    }
}
