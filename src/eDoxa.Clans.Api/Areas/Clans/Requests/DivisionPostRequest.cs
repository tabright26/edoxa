// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Requests
{
    [DataContract]
    public sealed class DivisionPostRequest
    {
        public DivisionPostRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }

#nullable disable
        public DivisionPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "description", IsRequired = false)]
        public string? Description { get; private set; }

    }
}

