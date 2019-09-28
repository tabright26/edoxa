// Filename: AccountDepositPostRequest.cs
// Date Created: 2019-08-27
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.Requests
{
    [DataContract]
    public sealed class CandidaturePostRequest
    {
        public CandidaturePostRequest(UserId userId, ClanId clanId)
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
        public UserId UserId { get; private set; }

        [DataMember(Name = "clanId")]
        public ClanId ClanId { get; private set; }

    }
}

