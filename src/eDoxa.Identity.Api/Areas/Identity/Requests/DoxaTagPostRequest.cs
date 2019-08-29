// Filename: DoxatagPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public sealed class DoxaTagPostRequest
    {
        public DoxaTagPostRequest(string name)
        {
            Name = name;
        }

#nullable disable
        public DoxaTagPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}
