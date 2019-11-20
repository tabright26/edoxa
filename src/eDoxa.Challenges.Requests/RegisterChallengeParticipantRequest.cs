// Filename: RegisterChallengeParticipantRequest.cs
// Date Created: 2019-11-08
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace eDoxa.Challenges.Requests
{
    [DataContract]
    public sealed class RegisterChallengeParticipantRequest
    {
        public RegisterChallengeParticipantRequest(Guid participantId)
        {
            ParticipantId = participantId;
        }

#nullable disable
        public RegisterChallengeParticipantRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "participantId")]
        public Guid ParticipantId { get; private set; }
    }
}
