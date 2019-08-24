﻿// Filename: DoxatagPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class DoxaTagPostRequest
    {
        public DoxaTagPostRequest(string name)
        {
            Name = name;
        }

        public DoxaTagPostRequest()
        {
            // Required by Fluent Validation.
        }

        [DataMember(Name = "name")]
        public string Name { get; private set; }
    }
}