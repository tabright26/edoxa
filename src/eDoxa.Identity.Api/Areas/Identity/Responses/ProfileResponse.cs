// Filename: ProfileResponse.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public class ProfileResponse
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthDate")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? BirthDate { get; set; }
    }
}
