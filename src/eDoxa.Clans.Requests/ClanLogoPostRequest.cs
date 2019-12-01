// Filename: ClanLogoPostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace eDoxa.Clans.Requests
{
    [JsonObject]
    public sealed class ClanLogoPostRequest
    {
        [JsonConstructor]
        public ClanLogoPostRequest(IFormFile logo)
        {
            Logo = logo;
        }

        public ClanLogoPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("logo")]
        public IFormFile Logo { get; private set; }
    }
}
