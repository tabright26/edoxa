// Filename: ClanLogoPostRequest.cs
// Date Created: 2019-09-30
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Clans.Requests
{
    [DataContract]
    public sealed class ClanLogoPostRequest
    {
        public ClanLogoPostRequest(IFormFile logo)
        {
            Logo = logo;
        }

#nullable disable
        public ClanLogoPostRequest() //Todo: Why is this never used.
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "logo")]

        public IFormFile Logo { get; private set; }
    }
}
