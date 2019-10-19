// Filename: InformationsPostRequest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Application.Requests;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public sealed class InformationsPostRequest
    {
        public InformationsPostRequest(
            string firstName,
            string lastName,
            Gender gender,
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

        public InformationsPostRequest()
        {
            // Required by Fluent Validation.
        }

        [DataMember(Name = "firstName")]
        public string FirstName { get; private set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; private set; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; private set; }

        [DataMember(Name = "dob")]
        public DobRequest Dob { get; private set; }
    }
}
