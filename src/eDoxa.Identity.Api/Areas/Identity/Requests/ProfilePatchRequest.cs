// Filename: ProfilePatchRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class ProfilePatchRequest
    {
        public ProfilePatchRequest(
            string? firstName,
            string? lastName,
            Gender? gender,
            DateTime? birthDate
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
        }

        [DataMember(Name = "firstName", IsRequired = false)]
        public string? FirstName { get; private set; }

        [DataMember(Name = "lastName", IsRequired = false)]
        public string? LastName { get; private set; }

        [DataMember(Name = "gender", IsRequired = false)]
        public Gender? Gender { get; private set; }

        [DataMember(Name = "birthDate", IsRequired = false)]
        public DateTime? BirthDate { get; private set; }
    }
}
