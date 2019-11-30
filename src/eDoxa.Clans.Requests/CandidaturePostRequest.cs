// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace eDoxa.Clans.Requests
{
    [DataContract]
    public sealed class CandidaturePostRequest
    {
        public CandidaturePostRequest(Guid userId, Guid clanId)
        {
            UserId = userId;
            ClanId = clanId;
        }

#nullable disable
        public CandidaturePostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "userId")]
        public Guid UserId { get; private set; }

        [DataMember(Name = "clanId")]
        public Guid ClanId { get; private set; }

    }
}

