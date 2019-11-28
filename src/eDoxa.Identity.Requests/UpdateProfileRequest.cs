// Filename: InformationsPutRequest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class UpdateProfileRequest
    {
        public UpdateProfileRequest(string firstName)
        {
            FirstName = firstName;
        }

#nullable disable
        public UpdateProfileRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "firstName")]
        public string FirstName { get; private set; }
    }
}
