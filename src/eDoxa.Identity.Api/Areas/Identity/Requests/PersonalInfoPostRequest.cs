// Filename: PersonalInfoPostRequest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

using eDoxa.Identity.Api.Infrastructure.Models;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class PersonalInfoPostRequest
    {
        public PersonalInfoPostRequest(
            string firstName,
            string lastName,
            Gender gender,
            DateTime birthDate
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
        }

        [DataMember(Name = "firstName")]
        public string FirstName { get; }

        [DataMember(Name = "lastName")]
        public string LastName { get; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate { get; }
    }
}
