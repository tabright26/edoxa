// Filename: DoxatagPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class DoxatagPutRequest
    {
        public DoxatagPutRequest(string name)
        {
            Name = name;
        }

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}
