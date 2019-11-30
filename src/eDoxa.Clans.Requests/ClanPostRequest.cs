// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Clans.Requests
{
    [DataContract]
    public sealed class ClanPostRequest
    {
        public ClanPostRequest(string name, string summary)
        {
            Name = name;
            Summary = summary;
        }

#nullable disable
        public ClanPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "summary", IsRequired = false)]
        public string? Summary { get; private set; }

    }
}

