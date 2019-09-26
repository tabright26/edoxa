// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Requests
{
    [DataContract]
    public sealed class CandidaturePostRequest
    {
        public CandidaturePostRequest(ClanId clanId)
        {
            ClanId = clanId;
        }

#nullable disable
        public CandidaturePostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "clanId")]
        public ClanId ClanId { get; private set; }

    }
}

