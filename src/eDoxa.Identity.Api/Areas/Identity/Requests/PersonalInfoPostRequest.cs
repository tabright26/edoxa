﻿// Filename: PersonalInfoPostRequest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;
using System.Runtime.Serialization;

using eDoxa.Identity.Api.Infrastructure.Models;

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

        public PersonalInfoPostRequest()
        {
            // Required by Fluent Validation.
        }

        [DataMember(Name = "firstName")]
        public string FirstName { get; private set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; private set; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; private set; }

        [DataMember(Name = "birthDate")]
        public DateTime BirthDate { get; private set; }
    }
}
