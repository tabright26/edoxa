// Filename: PersonalInfoPutRequest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class PersonalInfoPutRequest
    {
        public PersonalInfoPutRequest(string firstName)
        {
            FirstName = firstName;
        }

        public PersonalInfoPutRequest()
        {
            // Required by Fluent Validation.
        }

        [DataMember(Name = "firstName")]
        public string FirstName { get; private set; }
    }
}
