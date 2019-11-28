// Filename: InformationsPostRequest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class CreateProfileRequest
    {
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

        [DataMember(Name = "firstName")]
        public string FirstName { get; private set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; private set; }

        [DataMember(Name = "gender")]
        public string Gender { get; private set; }

        [DataMember(Name = "dob")]
        public DobRequest Dob { get; private set; }
    }
}
