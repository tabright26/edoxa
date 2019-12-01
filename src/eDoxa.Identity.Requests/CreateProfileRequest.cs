// Filename: CreateProfileRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class CreateProfileRequest
    {
        [JsonConstructor]
        public CreateProfileRequest(
            string firstName,
            string lastName,
            string gender,
            int year,
            int month,
            int day
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Dob = new DobRequest(year, month, day);
        }

        public CreateProfileRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("firstName")]
        public string FirstName { get; private set; }

        [JsonProperty("lastName")]
        public string LastName { get; private set; }

        [JsonProperty("gender")]
        public string Gender { get; private set; }

        [JsonProperty("dob")]
        public DobRequest Dob { get; private set; }
    }
}
