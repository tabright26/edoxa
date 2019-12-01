// Filename: RegisterChallengeParticipantRequest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Requests
{
    [JsonObject]
    public sealed class RegisterChallengeParticipantRequest
    {
        [JsonConstructor]
        public RegisterChallengeParticipantRequest(Guid participantId)
        {
            ParticipantId = participantId;
        }

        public RegisterChallengeParticipantRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("participantId")]
        public Guid ParticipantId { get; private set; }
    }
}
