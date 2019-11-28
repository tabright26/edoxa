// Filename: DoxatagPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class DoxatagPostRequest
    {
        public DoxatagPostRequest(string name)
        {
            Name = name;
        }

#nullable disable
        public DoxatagPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}
