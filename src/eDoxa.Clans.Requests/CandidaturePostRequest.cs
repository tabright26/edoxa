// Filename: CandidaturePostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Clans.Requests
{
    [JsonObject]
    public sealed class CandidaturePostRequest
    {
        [JsonConstructor]
        public CandidaturePostRequest(Guid userId, Guid clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

        public CandidaturePostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("userId")]
        public Guid UserId { get; private set; }

        [JsonProperty("clanId")]
        public Guid ClanId { get; private set; }
    }
}
