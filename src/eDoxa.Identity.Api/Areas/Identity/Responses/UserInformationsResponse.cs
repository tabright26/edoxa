// Filename: InformationsResponse.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Seedwork.Application.Responses;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public class UserInformationsResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dob")]
        public DobResponse Dob { get; set; }
    }
}
